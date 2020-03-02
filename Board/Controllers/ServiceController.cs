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

        //[HttpGet]
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

    
        public FileResult DownloadZipFile()
        {

            var fileName = string.Format("{0}_ImageFiles.zip", DateTime.Today.Date.ToString("dd-MM-yyyy") + "_1");
            var tempOutPutPath = Path.Combine(_hostEnvironment.WebRootPath, "images" + fileName);

            
            using (ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(@"D:\Examp")))
            {
                s.SetLevel(9);   

                byte[] buffer = new byte[4096];

                var ImageList = new List<string>();

               
                for (int i = 0; i < ImageList.Count; i++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(ImageList[i]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);

                    using (FileStream fs = System.IO.File.OpenRead(ImageList[i]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Flush();
                s.Close();

            }

            byte[] finalResult = System.IO.File.ReadAllBytes(@"D:\Examp");
            if (System.IO.File.Exists(tempOutPutPath))
                System.IO.File.Delete(tempOutPutPath);

            if (finalResult == null || !finalResult.Any())
                throw new Exception(String.Format("No Files found with Image"));

            return File(finalResult, "application/zip", fileName);

        }
    }
}


