using System;
using System.Net;
using ErickExample.Interfaces;

namespace ErickExample.Classes
{
    class HttpFileDownloader : IFileDownloader
    {
        public string Download(Uri file)
        {
            return new WebClient().DownloadString(file);
        }
    }
}
