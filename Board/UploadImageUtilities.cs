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
            string uploadsFolder = outputPath;
            string uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return uniqueFileName;
        }

        public static void DeleteImage(this IFormFile file, string outputPath)
        {
            FileInfo fileInf = new FileInfo(outputPath);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
    }
}
