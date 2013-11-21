using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;

namespace System.Web.Mvc
{

    /// <summary>
    /// Mvc域策略配置器
    /// 1：优先使用配置数据；当配置数据无效时，使用代码中的默认数据。
    /// 2：实际使用的动态库版本号要与配置的版本号一致，否则报错。
    /// 3：配置Domain约定为继承DreamAreaRegistration的实例名称
    /// 4：Mvc站点中Web.Config的AppSettings节点中必须有WinAreaRegistration配置项，配置相应的xml文件的相对路径（xml文件格式参见属性AreaRegistrationXmlDesc）
    /// </summary>
    public abstract class WinAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return Config.Area;
            }
        }
        private SynAreaConfig config = new SynAreaConfig("");
        private SynAreaConfig Config
        {
            get
            {
                if (!config.IsValid)
                    config = LoadAreaConfig(this.GetType().Name, GetVersion());
                return config;
            }
        }
        private string GetVersion()
        {
            Match match = Regex.Match(this.GetType().AssemblyQualifiedName, "Version=[\\d.]+");
            if (!match.Success) return string.Empty;
            match = Regex.Match(match.Value, "[\\d.]+");
            if (!match.Success) return string.Empty;
            return match.Value;
        }

        /// <summary>
        /// 路由规则（area/{controller}/{action}/{id}）
        /// </summary>
        protected virtual string Url { get { return "/{controller}/{action}/{id}"; } }
        /// <summary>
        /// 路由规则默认值( new { controller = "Main", action = "Index", id = UrlParameter.Optional })
        /// </summary>
        protected virtual object Default { get { return new { controller = "Main", action = "Index", id = UrlParameter.Optional }; } }
        /// <summary>
        /// 指定URl参数的有效值表达式（null）（该项不做配置）
        /// </summary>
        protected virtual object Constraints { get { return null; } }
        /// <summary>
        /// 默认的路由规则命名空间（null）
        /// </summary>
        protected virtual string[] Namespaces { get { return null; } }

        /// <summary>
        /// 域配置xml文件格式描述
        /// </summary>
        protected string AreaRegistrationXmlDesc
        {
            get
            {
                return "<SynMvcAreaConfigurations>" +
                            "<SynMvcArea Name=\"测试\" Code=\"Test\" Domain=\"TestAreaRegistration\" Area=\"Test\" Url=\"/{controller}/{action}/{id}\" DefController=\"\" DefAction=\"\" DefParam=\"\" Version=\"1.0.0.0\" />" +
                                "<NameSpaces>" +
                                    "<NameSpace Id=\"936c879b-5c98-46cd-a419-7d5e5618d8cd\" Guid=\"4392cb95-12c9-4ba7-afb2-0377ab7ae5a7\" Type=\"System.Test.a\">System.Test.a</NameSpace>" +
                                    "<NameSpace Id=\"936c879b-5c98-46cd-a419-7d5e5618d8cd\" Guid=\"4392cb95-12c9-4ba7-afb2-0377ab7ae5a7\" Type=\"System.Test.b\">System.Test.b</NameSpace>" +
                                "</NameSpaces>" +
                            "</SynMvcArea>" +
                            "<SynMvcArea Name=\"测试1\" Code=\"Test1\" Domain=\"Test1AreaRegistration\" Area=\"Test1\" Url=\"/{controller}/{action}/{id}\" DefController=\"\" DefAction=\"\" DefParam=\"\" Version=\"1.0.0.0\" />" +
                        "</SynMvcAreaConfigurations>";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (Config.IsValid)
            {
                context.MapRoute(
                    Config.Area + "_default_" + Guid.NewGuid(),
                    Config.Area + "/{controller}/{action}/{id}",
                    new { controller = "Main", action = "Index", id = UrlParameter.Optional },
                    Constraints,
                    Config.IsValidNameSpaces ? Config.NameSpaces : Namespaces
                );
            }
            else throw new Exception("Error when RegisterArea: Unvalid Area Configuration for " + this.GetType().FullName);
        }

        private static XmlDocument doc = null;
        private static SynAreaConfig LoadAreaConfig(string area, string version)
        {
            if (doc == null)
            {
                doc = new XmlDocument();
                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.Web.Configuration.WebConfigurationManager.AppSettings["WinAreaRegistration"]);
                string xml = File.ReadAllText(filepath);
                doc.LoadXml(xml);
            }
            XmlElement root = doc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                if (area == node.Attributes["Domain"].Value && version == node.Attributes["Version"].Value)
                {
                    SynAreaConfig config = new SynAreaConfig(version);
                    config.Area = node.Attributes["Area"].Value;
                    config.DefController = node.Attributes["DefController"].Value;
                    config.DefAction = node.Attributes["DefAction"].Value;
                    config.DefParam = node.Attributes["DefParam"].Value;
                    config.Version = node.Attributes["Version"].Value;

                    List<string> namespaces = new List<string>();
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        if (!string.IsNullOrEmpty(n.InnerText))
                            namespaces.Add(n.InnerText);
                    }
                    config.NameSpaces = namespaces.ToArray();
                    return config;
                }
            }
            return new SynAreaConfig(version);
        }
    }

    internal class SynAreaConfig
    {
        public SynAreaConfig(string realVersion) { realversion = realVersion; }
        public string Area { get; set; }
        public string Url { get; set; }
        public string DefController { get; set; }
        public string DefAction { get; set; }
        public string DefParam { get; set; }
        public string[] NameSpaces { get; set; }
        public string Version { get; set; }
        private string realversion = "";
        public string RealVersion { get { return realversion; } }
        public bool IsValid { get { return !string.IsNullOrEmpty(Area) && Version == RealVersion; } }
        public bool IsValidUrl { get { return !string.IsNullOrEmpty(Url); } }
        public bool IsValidNameSpaces { get { return NameSpaces != null && NameSpaces.Length > 0; } }
    }
}
