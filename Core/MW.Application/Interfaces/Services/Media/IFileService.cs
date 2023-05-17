﻿using Microsoft.AspNetCore.Http;
using MW.Domain.Enums.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Interfaces.Services.Media
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadRangeAsync(IFormFileCollection formFileColletion);
        Task<List<(string fileName, string path)>> UploadRangeAsync(IFormFileCollection formFileColletion, string specialName);
        Task<List<(string fileName, string path)>> UploadRangeAsync(IFormFileCollection formFileColletion, string specialName, RegisteredFileType registeredFileType);
        Task<(string fileName, string path)> UploadAsync(IFormFile formFile);
        Task<(string fileName, string path)> UploadAsync(IFormFile formFile, string specialName);
        Task<(string fileName, string path)> UploadAsync(IFormFile formFile, string specialName, RegisteredFileType registeredFileType);
        Task CopyFileAsync(string path, IFormFile file);
        Task DeleteAsync(string path, string fileName);
        List<string> GetFiles(string path);
        bool HasFile(string path, string fileName);
        Task<string> FileRenameAsync(string fileName, string path, bool overwrite = false);
    }
}
