using System;
using ErickExample.Interfaces;

namespace ErickExample.Classes
{
    class StringParsingTitleScraper : ITitleScraper
    {
        public string Scrape(string fileContents)
        {
            var title = string.Empty;
            var openingTagIndex = fileContents.IndexOf("<title>", StringComparison.Ordinal);
            var closingTagIndex = fileContents.IndexOf("</title>", StringComparison.Ordinal);

            if (openingTagIndex != -1 && closingTagIndex != -1)
                title = fileContents.Substring(openingTagIndex,
                    closingTagIndex - openingTagIndex).Substring(7);

            return title;
        }
    }
}
