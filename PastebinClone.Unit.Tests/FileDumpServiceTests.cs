using System;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using PastebinClone.Configuration;
using PastebinClone.Models;
using PastebinClone.Services;
using Xunit;

namespace PastebinClone.Unit.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("abcd", "./TestData/abcd/Content.3.html")]
        [InlineData("defg", "./TestData/defg/Content.html")]
        [InlineData("bcde", null)]
        public void GetHtmlFiles_Returns_HighestVersionHtml(string directory, string? expexted)
        {
            var testFiles = Options.Create(new DirectoryLocation()
            {
                Path = "./TestData/"
            });
            var sut = new FileDumpService(new FileSystem(),testFiles);

            var result = sut.GetFiles(new ContentModel() { Content = directory }, "html");

            result.Should().Be(expexted);
        }
    }
}