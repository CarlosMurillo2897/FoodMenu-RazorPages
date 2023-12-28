using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace FoodMenu.Utilities
{
    public class FileHelper
    {
        private string _webRootPath { get; set; }
        public FileHelper(string webRootPath)
        {
            _webRootPath = webRootPath;
        }
        public string CopyFile(IFormFileCollection files)
        {
            string newFileName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(files[0].FileName);
            var uploads = Path.Combine(_webRootPath, TrimSlash(Constants.ImagesPath));
            var fullPath = Path.Combine(uploads, $"{newFileName}{extension}");

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            return $"{Constants.ImagesPath}{newFileName}{extension}";
        }

        public void RemoveFile(string image)
        {
            var oldImagePath = Path.Combine(_webRootPath, TrimSlash(image));
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }

        private string TrimSlash(string path) => path.Trim('\\');
    }
}
