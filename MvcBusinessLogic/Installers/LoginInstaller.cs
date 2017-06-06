using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcBusinessLogic.Classes;
using MvcBusinessLogic.Interfaces;

namespace MvcBusinessLogic.Installers
{
    public class LoginInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<LoginController>());
            container.Register(Component.For<HttpFileDownloader>());
            container.Register(Component.For<ILoginService>().ImplementedBy<LoginService>());
            container.Register(Component.For<ISecurity>().ImplementedBy<Security>().LifestyleTransient());
        }
    }
}
