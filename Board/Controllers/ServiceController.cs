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
        
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult GenerateDowloadZip()
        {
            Photo ser = new Photo();

            ConfigBuilder b1 = new LocalServiceConfigBuilder();
            ser.Construct(b1);
            ServiceConfig p = b1.GetResult();

            MyService my = new MyService(p);
            my.CreateBackup();

            return View();
        }

    }
}

