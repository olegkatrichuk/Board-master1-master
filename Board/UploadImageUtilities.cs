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
      var create = new FileManager.CreateMyFile();
      FileManager fileManager = new FileManager(create, outputPath,file);
      fileManager.WorkWithFile();
      return create.Result;
    }

    public static void DeleteImage(this IFormFile file, string outputPath)
    {
      FileManager fileManager = new FileManager(new FileManager.DeleteMyFile(), outputPath,file);
      fileManager.WorkWithFile();
    }
  }
}
