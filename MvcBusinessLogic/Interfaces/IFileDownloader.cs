using System;

namespace MvcBusinessLogic.Interfaces
{
    public interface IFileDownloader
    {
        string Download(Uri file);
    }
}
