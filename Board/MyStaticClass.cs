using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
    public  static class MyStaticClass
    {
        public static string GetLinkToDownloadFile(this IFormFile file, string pathGet)
        {
            string path = @"D:/Test";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            try
            {
                using FileStream fs = new FileStream($"{path}.jpg",FileMode.Create);
                file.CopyTo(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return path;
        }

    }
}
