using System;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Web.Routing;
using Autofac;
using Autofac.Extras.Multitenant;
using Autofac.Extras.Multitenant.Wcf;
using Autofac.Integration.Wcf;
using WinStudio.WcfAutofac.Services;

namespace WinStudio.WcfAutofacDemo
{
    /// <summary>
    /// Global application class for the multitenant WCF example application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is easiest to see this application in action from the MVC example.
    /// The MVC example makes use of this service and displays the information,
    /// illustrating a complete multitenant system in action.
    /// </para>
    /// </remarks>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the global application startup event.
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            Type type = typeof(ServiceCustomer);

            var builder = new ContainerBuilder();
            builder.RegisterType(type);
            builder.RegisterType<ServiceFactory>();
            AutofacHostFactory.Container = builder.Build();
            var host = new AutofacServiceHostFactory();
            RouteTable.Routes.Add(new ServiceRoute("ServiceCustomer", host, type));
            RouteTable.Routes.Add(new ServiceRoute("ServiceFactory", host, typeof(ServiceFactory)));
        }
    }
}