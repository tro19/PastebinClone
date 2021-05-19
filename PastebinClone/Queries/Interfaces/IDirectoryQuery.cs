using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;

namespace PastebinClone.Queries.Interfaces
{
    public interface IDirectoryQuery
    {
        Task<IActionResult> GetFile(ContentModel contentModel);
    }
}