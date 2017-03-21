using System;

namespace ErickExample.Interfaces
{
    interface IFileDownloader
    {
        string Download(Uri file);
    }
}
