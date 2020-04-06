using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using MyBoard.Helpers;
using MyBoard.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace MyBoard.Controllers
{
  
  public class ImagesController : Controller
    {
    private readonly AzureStorageConfig _storageConfig;

    public ImagesController(IOptions<AzureStorageConfig> config)
    {

      _storageConfig = config.Value;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(ICollection<IFormFile> files)
    {
      string isUploaded = "";
      
      try
      {
        if (files.Count == 0)
          return BadRequest("No files received from the upload");

        if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
          return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

        if (_storageConfig.ImageContainer == string.Empty)
          return BadRequest("Please provide a name for your image container in the azure blob storage");

        if (_storageConfig.ThumbnailContainer == string.Empty)
          return BadRequest("Please provide a name for your image container in the azure blob storage");

        foreach (var formFile in files)
        {
          
          if (StorageHelper.IsImage(formFile))
          {
            if (formFile.Length > 0)
            {
              isUploaded = await StorageHelper.UploadFileToStorage(formFile.OpenReadStream(), formFile.FileName, _storageConfig);
            }
          }
          else
          {
            return new UnsupportedMediaTypeResult();
          }
        }

        if (isUploaded.Any())
        {
          return Ok(isUploaded);
        }

        return BadRequest("Look like the image could not upload to the storage");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

  }
}