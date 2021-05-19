using System.Threading.Tasks;
using AngleSharp.Dom;

namespace PastebinClone.Formatters.Interfaces
{
    public interface IHtmlFormatter
    {
        Task<IDocument> HtmlTemplate(string filePath);
    }
}