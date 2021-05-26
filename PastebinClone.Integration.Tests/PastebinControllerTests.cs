using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace PastebinClone.Integration.Tests
{
    public class PasteBinControllerTests
    {
        [Fact]
        public async Task Filedump_Returns_Ok()
        {
            var directory= "abcd";
            var client = new WebApplicationFactory<Startup>().CreateClient();

            var result = await client.GetAsync("/filedump/getcontent?content=" + directory);
            var html = await result.Content.ReadAsStringAsync();
            
            result.IsSuccessStatusCode.Should().BeTrue();
            html.Should().Be("<html><head><title>Content.3.html</title></head><body><p>Content 3</p></body></html>");
        }
    }
}