using PastebinClone.Models;

namespace PastebinClone.Services.Interfaces
{
    public interface IFileDumpService
    {
        string GetFiles(ContentModel contentModel, string extension);

        bool DirectoryExists(ContentModel contentModel);
    }
}