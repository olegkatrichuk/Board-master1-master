using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBoard.Controllers
{

  public class ServiceController : Controller
  {
    private readonly IWebHostEnvironment _hostEnvironment;

    public ServiceController(IWebHostEnvironment hostEnvironment)
    {
      _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
      PhotoService ser = new PhotoService();

      PhotoServiceBilder bilder1 = new OneBuilder();
      Service ps = ser.Result(bilder1);
      ps.GetResult();


      PhotoServiceBilder bilder2 = new TwoBilder();
      Service ps2 = ser.Result(bilder2);
      ps2.GetResult();

      return View();
    }

    public FileResult GenerateAndDowloadZip()
    {
      var wedRoot = _hostEnvironment.WebRootPath;
      var fileName = "MyZip.zip";
      var tempOutput = wedRoot + "/picture/" + fileName;

      using (ZipOutputStream zipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
      {
        zipOutputStream.SetLevel(9);

        byte[] buffer = new byte[4096];

        var imageList = new List<string>();

        imageList.Add(wedRoot + "/picture/1.jpg");
        imageList.Add(wedRoot + "/picture/2.jpg");
        imageList.Add(wedRoot + "/picture/3.jpg");
        imageList.Add(wedRoot + "/picture/4.jpg");

        for (int i = 0; i < imageList.Count; i++)
        {
          ZipEntry entry = new ZipEntry(Path.GetFileName(imageList[i]));
          entry.DateTime = DateTime.Now;
          entry.IsUnicodeText = true;

          zipOutputStream.PutNextEntry(entry);

          using FileStream fileStream = System.IO.File.OpenRead(imageList[i]);
          int sourceByte;
          do
          {
            sourceByte = fileStream.Read(buffer, 0, buffer.Length);
            zipOutputStream.Write(buffer, 0, sourceByte);
          } while (sourceByte > 0);
        }
        zipOutputStream.Finish();
        zipOutputStream.Flush();
        zipOutputStream.Close();

      }

      byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);
      if (System.IO.File.Exists(tempOutput))
      {
        System.IO.File.Delete(tempOutput);
      }

      if (finalResult == null || !finalResult.Any())
      {
        throw new Exception(String.Format("Nothing Found"));
      }

      return File(finalResult, "application/zip", fileName);
    }
    

  }
}

