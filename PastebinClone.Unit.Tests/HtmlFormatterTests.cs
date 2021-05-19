using System.Threading.Tasks;
using AngleSharp.Dom;
using FluentAssertions;
using PastebinClone.Formatters;
using Xunit;

namespace PastebinClone.Unit.Tests
{
    public class HtmlFormatterTests
    {
        [Fact]
        public async Task GetTemplate_Returns_IDocument()
        {
            var sut = new HtmlFormatter();

            var result = await sut.HtmlTemplate("./TestData/abcd/Content.html");

            result.Should().NotBeNull();
        }
    }
}