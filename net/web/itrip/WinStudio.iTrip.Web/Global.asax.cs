using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MongoDB.Repository;
using WinStudio.iTrip.Models;
using WinStudio.Permission;

namespace WinStudio.iTrip.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public const string NotificationFlag = "NotificationFlag";
        private static List<HttpContextBase> contexts = new List<HttpContextBase>();
        public static bool NotificationState = true;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            MongoDBRepository.RegisterMongoDBContext(new iTripModelContext());
            MongoDBRepository.RegisterMongoIndex();

            MongoEntity.RemoveAll<Nationality>();
            MongoEntity.RemoveAll<NativePlace>();

            var initializer = new Initializer();
            var nations = initializer.InitializeNationality();
            MongoEntity.InsertBatch<NativePlace>(initializer.InitializeNativePlace(nations[0]));
            MongoEntity.InsertBatch<Nationality>(nations);

            Initialize();
        }

        private void Initialize()
        {
            PermissionConfiguration config = new PermissionConfiguration();
            config.WinTokenName = ConfigurationManager.AppSettings["WinTokenName"].ToString();
            config.WinTokenDomain = ConfigurationManager.AppSettings["WinTokenDomain"].ToString();
            config.WinTokenTimeout = ConfigurationManager.AppSettings["WinTokenTimeout"].ToInt(20);

            config.WinHandleUnAuthorizedAddress = ConfigurationManager.AppSettings["WinHandleUnAuthorizedAddress"].ToString();
            //config.WinHandleProtalAddress = ConfigurationManager.AppSettings["WinHandleProtalAddress"].ToString();
            //config.WinHandleLoginAddress = ConfigurationManager.AppSettings["WinHandleLoginAddress"].ToString();
            //config.WinHandleLogoutAddress = ConfigurationManager.AppSettings["WinHandleLogoutAddress"].ToString();
            config.WinHandleValidatationAddress = ConfigurationManager.AppSettings["WinHandleValidatationAddress"].ToString();
            config.WinHandleUpdateInfoAddress = ConfigurationManager.AppSettings["WinHandleUpdateInfoAddress"].ToString();
            //config.WinTmpException = ConfigurationManager.AppSettings["WinTmpException"].ToString();
            WinWebGlobalManager.Initialize(config, null, null);
        }
    }
}