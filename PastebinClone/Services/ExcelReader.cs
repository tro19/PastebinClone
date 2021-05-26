using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Models;
using PastebinClone.Services.Interfaces;

namespace PastebinClone.Services
{
    public class ExcelReader : IReaders
    {
        private IFileDumpService _fileDumpService;

        public ExcelReader(IFileDumpService fileDumpService)
        {
            _fileDumpService = fileDumpService;
        }

        public async Task<IActionResult> GetFile(ContentModel contentModel)
        {
            var filePath = _fileDumpService.GetFiles(contentModel, "xlsx");

            if (filePath is not null)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var result = new byte[fileStream.Length];
                    await fileStream.ReadAsync(result, 0, (int) fileStream.Length);
                    return new FileContentResult(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");   
                }
            }

            return null;
        }
    }
}