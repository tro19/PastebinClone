using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;

namespace PastebinClone.Services.Interfaces
{
    public interface IFileCoordinator
    {
        Task<IActionResult> GetPDF(ContentModel contentModel);
        Task<IActionResult> GetExcel(ContentModel contentModel);
        Task<IActionResult> GetHtml(ContentModel contentModel);
    }
}