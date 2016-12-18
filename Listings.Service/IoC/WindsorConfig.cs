using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Listings.Service.IoC
{
    public static class WindsorConfig
    {
        public static IWindsorContainer InstallWindsor(HttpConfiguration config, params IWindsorInstaller[] installers)
        {
            var container = new WindsorContainer().Install(installers);

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));

            return container;
        }
    }
}
