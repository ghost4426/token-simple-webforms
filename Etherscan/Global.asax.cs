using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using Service.Repository;
using Service.Services;
using Unity;

namespace Etherscan
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = this.AddUnity();
            container.RegisterType<ITokenService, TokenService>();
            container.RegisterType<ITokenRepository, TokenRepository>();
        }

    }
}