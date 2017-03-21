using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ErickExample.Classes;
using ErickExample.Interfaces;

namespace ErickExample.Installers
{
    internal class UrlInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<HtmlTitleRetriever>());
            container.Register(Component.For<IFileDownloader>().ImplementedBy<HttpFileDownloader>());
            container.Register(Component.For<ITitleScraper>().ImplementedBy<StringParsingTitleScraper>());
        }
    }
}
