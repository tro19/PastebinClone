using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;

namespace PastebinClone.Services.Interfaces
{
    public interface IFileCoordinator
    {
        Task<IActionResult> FileEnumerator(ContentModel contentModel);
    }
}