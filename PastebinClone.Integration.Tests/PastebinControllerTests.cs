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
            var expected = "thisisakey";
            var client = new WebApplicationFactory<Startup>().CreateClient();

            var result = await client.GetAsync("/filedump/getcontent?content=" + expected);
            var key = await result.Content.ReadAsStringAsync();
            
            result.IsSuccessStatusCode.Should().BeTrue();
            key.Should().Be(expected);
        }
    }
}