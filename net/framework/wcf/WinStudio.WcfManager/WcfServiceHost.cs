using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.WcfManager
{
    /// <summary>
    /// 主机信息
    /// </summary>
    public class HostInfo : IDisposable
    {
        /// <summary>
        /// 主机信息
        /// </summary>
        /// <param name="server">主机地址（含端口，但不包含http、https，如127.0.0.1:8888、mydomain）</param>
        /// <param name="ishttp">标识是否为http（false为https）</param>
        public HostInfo(string server, bool ishttp = true)
        {
            _server = server;
            _ishttp = ishttp;
        }
        private string _server;
        private bool _ishttp = true;
        private List<Type> _types = new List<Type>();

        public bool IsHttpGet { get { return _ishttp; } }
        public string Server { get { return _server; } }

        /// <summary>
        /// 得到客户端可用的请求地址
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns></returns>
        public virtual Uri GetServiceTypeUri(Type type)
        {
            return new Uri(string.Format("{0}://{1}/Services/{2}", IsHttpGet ? "http" : "https", Server, type.Name));
        }
        /// <summary>
        /// 得到客户端可用的元数据请求地址
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns></returns>
        public virtual Uri GetServiceTypeMetadataGetUri(Type type)
        {
            return new Uri(string.Format("{0}://{1}/Services/{2}/wsdl", IsHttpGet ? "http" : "https", Server, type.Name));
        }

        /// <summary>
        /// 为主机增加一个服务类型
        /// </summary>
        /// <param name="types">服务类型</param>
        public HostInfo AddTypes(params Type[] types)
        {
            _types.AddRange(types);
            return this;
        }

        /// <summary>
        /// 该主机下所有的服务类型
        /// </summary>
        public Type[] AllTypes
        {
            get { return _types.ToArray(); }
        }

        /// <summary>
        /// 根据程序集加载服务类型（需使用WinWcfServiceAttribute标识服务类型）
        /// </summary>
        /// <param name="assemblies">程序集集合</param>
        public HostInfo LoadTypesFromAssemblies(params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0) return this;
            foreach (var itype in assemblies.FindTypes<WinWcfServiceAttribute>())
            {
                var types = assemblies.FindTypes(itype.FullName);
                AddTypes(types);
            }
            return this;
        }

        /// <summary>
        /// 根据主机信息获取相应的服务类型
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <param name="bindingConfigName">BindingConfigName</param>
        /// <returns></returns>
        public T GetService<T>(string bindingConfigName = null)
        {
            if (!typeof(T).IsInterface)
                return default(T);
            if (typeof(T).GetCustomAttributes(typeof(ServiceContractAttribute), true).Length == 0)
                return default(T);

            var channelFactory = new ChannelFactory<T>(string.IsNullOrEmpty(bindingConfigName) ? new WSHttpBinding(SecurityMode.None) : new WSHttpBinding(bindingConfigName), GetServiceTypeUri(typeof(T)).AbsoluteUri);
            T proxy = channelFactory.CreateChannel();
            return proxy;
        }

        public void Dispose()
        {
            _types.Clear();
            _server = string.Empty;
            _ishttp = false;
        }
    }

    public class ServiceContainer : IDisposable
    {
        private List<ServiceHost> _hosts = new List<ServiceHost>();
        /// <summary>
        /// 服务启动前
        /// </summary>
        public EventHandler OnServiceStarting;
        /// <summary>
        /// 服务启动后
        /// </summary>
        public EventHandler OnServiceStarted;
        /// <summary>
        /// 服务关闭后
        /// </summary>
        public EventHandler OnServiceCloseed;

        /// <summary>
        /// 根据主机信息构建主机服务器
        /// </summary>
        /// <param name="hostinfo"></param>
        protected void Build(HostInfo hostinfo)
        {
            int count = 1;
            Console.WriteLine("-----Building {0}, {1} Services-----", hostinfo.Server, hostinfo.AllTypes.Length);
            foreach (var type in hostinfo.AllTypes)
            {
                if (_hosts.Any(h => h.Description.ServiceType == type)) continue;

                var host = new ServiceHost(type);
                Type it = GetInterfaceType(type);
                host.AddServiceEndpoint(it, new WSHttpBinding(SecurityMode.None), hostinfo.GetServiceTypeUri(it));

                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    if (hostinfo.IsHttpGet)
                    {
                        behavior.HttpGetEnabled = true;
                        behavior.HttpGetUrl = hostinfo.GetServiceTypeMetadataGetUri(it);
                    }
                    else
                    {
                        behavior.HttpsGetEnabled = true;
                        behavior.HttpsGetUrl = hostinfo.GetServiceTypeMetadataGetUri(it);
                    }
                    host.Description.Behaviors.Add(behavior);
                }
                host.Opened += delegate
                {
                    Console.WriteLine("*****{0} -> Opened*************************", host.Description.Name);
                    Console.WriteLine("*****ServiceType={0}", host.Description.ServiceType.FullName);
                    var metadata = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                    if (metadata != null)
                    {
                        if (metadata.HttpGetEnabled)
                            Console.WriteLine("*****MetadataHttpGetUrl={0}", metadata.HttpGetUrl);
                        if (metadata.HttpsGetEnabled)
                            Console.WriteLine("*****MetadataHttpsGetUrl={0}", metadata.HttpsGetUrl);
                    }
                    foreach (var endpoint in host.Description.Endpoints)
                    {
                        Console.WriteLine("*****-----Endpoint:{0}***************", endpoint.Name);
                        Console.WriteLine("*****-----Address.Uri={0}", endpoint.Address.Uri);
                        Console.WriteLine("*****-----ContractType={0}", endpoint.Contract.ContractType.FullName);
                    }

                    Console.WriteLine("*****Service {0} End************************************************************", count++);

                    if (OnServiceStarted != null)
                        OnServiceStarted(host.Description.Name, null);
                };
                host.Closed += delegate
                {
                    Console.WriteLine("*****{0} -> Closed*************************", host.Description.Name);
                    if (OnServiceCloseed != null)
                        OnServiceCloseed(host.Description.Name, null);
                };
                host.Opening += delegate
                {
                    Console.WriteLine("*****{0} -> Opening************************", host.Description.Name);
                    if (OnServiceStarting != null)
                        OnServiceStarting(host.Description.Name, null);
                };
                host.Faulted += delegate
                {
                    Console.WriteLine("*****{0} -> Faulted************************", host.Description.Name);
                };
                _hosts.Add(host);
            }
        }

        private Type GetInterfaceType(Type type)
        {
            return type.GetInterfaces().Where(i => i.GetCustomAttributes(typeof(ServiceContractAttribute), true).Length > 0).First();
        }

        /// <summary>
        /// 打开主机服务器
        /// </summary>
        /// <param name="hostinfo"></param>
        public void Open(HostInfo hostinfo)
        {
            if (_hosts.Count == 0)
                Build(hostinfo);
            foreach (var host in _hosts)
            {
                host.Open();
            }
        }

        /// <summary>
        /// 关闭主机服务器
        /// </summary>
        /// <param name="hostinfo"></param>
        public void Close(HostInfo hostinfo)
        {
            foreach (var host in _hosts)
            {
                host.Close();
            }
        }

        /// <summary>
        /// 释放主机服务器
        /// </summary>
        public void Dispose()
        {
            foreach (var host in _hosts)
            {
                host.Close();
            }
        }
    }

}
