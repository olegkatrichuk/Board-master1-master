﻿using System;
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

      BackupService backup = new BackupService(p);
      backup.CreateBackup();

      return View();
    }

    public IActionResult FileManagerService()
    {
      //string path = "";

      //FileManager fileManager = new FileManager(new FileManager.DeleteMyFile(), path);
      //fileManager.WorkWithFile();

      //fileManager.Files = new FileManager.CreateMyFile();
      //fileManager.WorkWithFile();

      return View();
    }

  }
}

