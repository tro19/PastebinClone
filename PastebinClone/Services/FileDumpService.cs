using System;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using PastebinClone.Configuration;
using PastebinClone.Models;
using PastebinClone.Services.Interfaces;

namespace PastebinClone.Services
{
    public class FileDumpService : IFileDumpService
    {
        private readonly IFileSystem _fileSystem;
        private readonly IOptions<DirectoryLocation> _options;
        
        public FileDumpService(IFileSystem fileSystem, IOptions<DirectoryLocation> options)
        {
            _fileSystem = fileSystem;
            _options = options;
        }

        public string GetFiles(ContentModel contentModel, string extension)
        {
            var latestVersion = GetVersionedFiles(contentModel, extension);

            if (string.IsNullOrEmpty(latestVersion))
                latestVersion = GetAllFilesWithExtension(contentModel, extension).FirstOrDefault();

            return latestVersion;
        }
        
        public bool DirectoryExists(ContentModel contentModel)
        {
            var path = CreatePath(contentModel);

            return _fileSystem.Directory.Exists(path);
        }

        private string GetVersionedFiles(ContentModel contentModel, string extension)
        {
            var regex = new Regex($".*\\.[0-9]*.{extension}");
            var digits = new Regex($".*\\.(?<version>[0-9]*).{extension}");

            var files = GetAllFilesWithExtension(contentModel, extension);
            var file = files.Where(x => regex.IsMatch(x))
                .OrderByDescending(x => digits.Match(x).Groups["version"].Value)
                .FirstOrDefault();
            return file;
        }

        private string[] GetAllFilesWithExtension(ContentModel contentModel, string extension)
        {
            return _fileSystem.Directory.GetFiles(CreatePath(contentModel), $"*.{extension}");
        }
        
        private string CreatePath(ContentModel contentModel)
        {
            return _options.Value.Path + contentModel.Content;
        }
    }
}