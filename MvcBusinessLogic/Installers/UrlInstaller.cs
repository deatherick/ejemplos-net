using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcBusinessLogic.Classes;
using MvcBusinessLogic.Interfaces;

namespace MvcBusinessLogic.Installers
{
    public class UrlInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<HtmlTitleRetriever>());
            container.Register(Component.For<IFileDownloader>().ImplementedBy<HttpFileDownloader>());
            container.Register(Component.For<ITitleScraper>().ImplementedBy<StringParsingTitleScraper>());
        }
    }
}
