using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;
using PastebinClone.Services.Interfaces;

namespace PastebinClone.Services
{
    public class PdfReader : IReaders
    {
        private IFileDumpService _fileDumpService;
        private const string FileType = "pdf";

        public PdfReader(IFileDumpService fileDumpService)
        {
            _fileDumpService = fileDumpService;
        }
        
        

        public async Task<IActionResult> GetFile(ContentModel contentModel)
        {
            var filePath = _fileDumpService.GetFiles(contentModel, "pdf");

            if (filePath is not null)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var result = new byte[fileStream.Length];
                    await fileStream.ReadAsync(result, 0, (int) fileStream.Length);
                    return new FileContentResult(result, "application/pdf");
                }
            }

            return null;
        }
    }
}