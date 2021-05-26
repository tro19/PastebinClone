using System.IO;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Formatters.Interfaces;
using PastebinClone.Models;
using PastebinClone.Services.Interfaces;

namespace PastebinClone.Services
{
    public class HtmlReader : IReaders
    {
        private IFileDumpService _fileDumpService;
        private IHtmlFormatter _htmlFormatter;

        public HtmlReader(IFileDumpService fileDumpService, IHtmlFormatter htmlFormatter)
        {
            _fileDumpService = fileDumpService;
            _htmlFormatter = htmlFormatter;
        }

        public async Task<IActionResult> GetFile(ContentModel contentModel)
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