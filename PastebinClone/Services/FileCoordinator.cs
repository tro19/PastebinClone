using System.IO;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Formatters.Interfaces;
using PastebinClone.Models;
using PastebinClone.Services.Interfaces;

namespace PastebinClone.Services
{
    public class FileCoordinator : IFileCoordinator
    {
        private readonly IFileDumpService _fileDumpService;
        private readonly IHtmlFormatter _htmlFormatter;

        public FileCoordinator(IFileDumpService fileDumpService,
                                IHtmlFormatter htmlFormatter)
        {
            _fileDumpService = fileDumpService;
            _htmlFormatter = htmlFormatter;
        }

        public async Task<IActionResult> GetPDF(ContentModel contentModel)
        {
            var filePath = _fileDumpService.GetFiles(contentModel, "pdf");

            return filePath is null ? null : new PhysicalFileResult(filePath, "application/pdf");
        }

        public async Task<IActionResult> GetExcel(ContentModel contentModel)
        {
            var filePath = _fileDumpService.GetFiles(contentModel, "xlsx");
            
            return filePath is null ?  null : new PhysicalFileResult(filePath,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        
        public async Task<IActionResult> GetHtml(ContentModel contentModel)
        {
            var htmlFile = _fileDumpService.GetFiles(contentModel, "html");

            if (htmlFile is not null)
            {
                var htmlDoc = await _htmlFormatter.HtmlTemplate(htmlFile);
            
                htmlDoc.Title = Path.GetFileName(htmlFile);

                return new ContentResult()
                {
                    Content = htmlDoc.ToHtml(),
                    ContentType = "text/html"
                };
            }

            return null;
        }
    }
}