using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcBusinessLogic.Classes;
using MvcBusinessLogic.Interfaces;

namespace MvcBusinessLogic.Installers
{
    public class DependenciesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Main>());
            container.Register(Component.For<IDependency1>().ImplementedBy<Dependency1>());
            container.Register(Component.For<IDependency2>().ImplementedBy<Dependency2>().LifestyleTransient());
        }
    }
}
