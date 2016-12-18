using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Listings.Service
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<ListingsController>()
                    .LifestyleTransient(),
                Component
                    .For<IListingsRepository>()
                    .ImplementedBy<ListingsRepository>()
                    .DependsOn(Dependency.OnValue(
                        "connectionString", ConfigurationManager.ConnectionStrings["Database"].ConnectionString)));
        }
    }
}
