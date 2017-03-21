using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ErickExample.Classes;
using ErickExample.Interfaces;

namespace ErickExample.Installers
{
    class LoginInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<LoginImplement>());
            container.Register(Component.For<ILoginController>().ImplementedBy<LoginController>());
            container.Register(Component.For<ISecurity>().ImplementedBy<Security>().LifestyleTransient());
        }
    }
}
