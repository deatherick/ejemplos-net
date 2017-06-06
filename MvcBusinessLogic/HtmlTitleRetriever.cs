using System;
using MvcBusinessLogic.Interfaces;

namespace MvcBusinessLogic
{
    public class HtmlTitleRetriever
    {
        private readonly IFileDownloader _dowloader;
        private readonly ITitleScraper _scraper;

        public HtmlTitleRetriever(IFileDownloader dowloader, ITitleScraper scraper)
        {
            _dowloader = dowloader;
            _scraper = scraper;
        }

        public string GetTitle(Uri file)
        {
            var fileContents = _dowloader.Download(file);
            return _scraper.Scrape(fileContents);
        }
    }
}
