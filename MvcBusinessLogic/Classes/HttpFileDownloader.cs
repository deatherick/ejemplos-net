using System;
using System.Net;
using MvcBusinessLogic.Interfaces;

namespace MvcBusinessLogic.Classes
{
    public class HttpFileDownloader : IFileDownloader
    {
        public string Download(Uri file)
        {
            try
            {
                return new WebClient().DownloadString(file);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
