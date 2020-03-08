using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
  public class FileManager
  {
    public IFiles Files { get; set; }

    protected string ThePath { get; set; }

    protected IFormFile FormFile { get; set; }

    public FileManager(IFiles files, string path, IFormFile formFile)
    {
      Files = files;
      ThePath = path;
      FormFile = formFile;
    }

    public void WorkWithFile()
    {
      Files.WorkWithFile(ThePath, FormFile);
    }

    public class DeleteMyFile : IFiles
    {
      public void WorkWithFile(string path, IFormFile file)
      {
        FileInfo fileInf = new FileInfo(path);
        if (fileInf.Exists)
        {
          fileInf.Delete();
        }
      }
    }

    public class CreateMyFile : IFiles
    {
      public string Result { get; set; }

      public void WorkWithFile(string outputPath, IFormFile file)
      {
        string uploadsFolder = outputPath;
        string uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using var fileStream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(fileStream);
        Result = uniqueFileName;
      }

    }
  }
}
