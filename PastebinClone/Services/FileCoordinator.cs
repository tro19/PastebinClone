using System.Collections.Generic;
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
        private IEnumerable<IReaders> _readers;
        public FileCoordinator(IEnumerable<IReaders> readers)
        {
            _readers = readers;
        }

        public async Task<IActionResult> FileEnumerator(ContentModel contentModel)
        {
            foreach (var reader in _readers)
            {
                var result = await reader.GetFile(contentModel);

                if (result is not null)
                    return result;
            }

            return null;
        }
    }
}