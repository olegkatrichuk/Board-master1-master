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
            MyService my = new MyService();
            my.Show();

            return View();
        }

    }
}

