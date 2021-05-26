using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;

namespace PastebinClone.Services.Interfaces
{
    public interface IReaders
    {
        Task<IActionResult> GetFile(ContentModel contentModel);
    }
}