using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;
using PastebinClone.Queries.Interfaces;
using PastebinClone.Services.Interfaces;

namespace PastebinClone.Queries
{
    public class DirectoryQuery : IDirectoryQuery
    {
        private readonly IFileCoordinator _fileCoordinator;
        private readonly IFileDumpService _fileDumpService;

        public DirectoryQuery(IFileCoordinator fileCoordinator,
                                IFileDumpService fileDumpService)
        {
            _fileCoordinator = fileCoordinator;
            _fileDumpService = fileDumpService;
        }

        public async Task<IActionResult> GetFile(ContentModel contentModel)
        {
            if (_fileDumpService.DirectoryExists(contentModel))
            {
                var response = await _fileCoordinator.GetHtml(contentModel);
               
                if (response is not IActionResult)
                    response = await _fileCoordinator.GetPDF(contentModel);
                
                if (response is not IActionResult)
                    response = await _fileCoordinator.GetExcel(contentModel);
                
                return response;
               
            }

            return null;
        }
    }
}