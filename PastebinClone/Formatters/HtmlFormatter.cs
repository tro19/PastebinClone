using System.IO;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using PastebinClone.Formatters.Interfaces;

namespace PastebinClone.Formatters
{
    public class HtmlFormatter : IHtmlFormatter
    {
        public async Task<IDocument> HtmlTemplate(string filepath)
        {
            var html = File.ReadAllText(filepath);
            var context = BrowsingContext.New(AngleSharp.Configuration.Default);
            return await context.OpenAsync(req => req.Content(html));
        }
    }
}