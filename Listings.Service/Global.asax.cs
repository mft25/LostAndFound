using System.Web;
using System.Web.Http;
using Castle.Windsor;
using Listings.Service.IoC;

namespace Listings.Service
{
    public class Global : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            // Windsor
            _container = WindsorConfig.InstallWindsor(GlobalConfiguration.Configuration, new WindsorInstaller());

            // Web API
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            _container.Dispose();
            Dispose();
        }
    }
}
