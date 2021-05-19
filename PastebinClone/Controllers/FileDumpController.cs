using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;
using PastebinClone.Queries.Interfaces;

namespace PastebinClone.Controllers
{
    public class FileDumpController : Controller
    {
        private readonly IDirectoryQuery _directoryQuery;

        public FileDumpController(IDirectoryQuery directoryQuery)
        {
            _directoryQuery = directoryQuery;
        }

        [HttpGet("/filedump/getcontent")]
        public async Task<IActionResult> Filedump([FromQuery] ContentModel contentModel)
        {
            var result = await _directoryQuery.GetFile(contentModel);

            return result ?? NotFound($"No content found in directory {contentModel.Content}");
        }
    }   
}