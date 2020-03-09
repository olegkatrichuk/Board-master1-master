using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
    public static class UploadImageUtilities
    {

        public static string UploadImage(this IFormFile file, string outputPath)
        {
            IFileManager fileManager = new LocalFileManager();
            return fileManager.UploadFile(outputPath, file);
        }

        public static void DeleteImage(string outputPath)
        {
            IFileManager fileManager = new LocalFileManager();
            fileManager.DeleteFile(outputPath);
        }
    }
}
