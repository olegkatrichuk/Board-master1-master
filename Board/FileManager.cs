using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
    public class LocalFileManager : IFileManager
    {
        public string UploadFile( string path, IFormFile formFile)
        {
            string uploadsFolder = path;
            string uniqueFileName = Guid.NewGuid() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            formFile.CopyTo(fileStream);
            return uniqueFileName;
        }

        public void DeleteFile(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
    }
}

