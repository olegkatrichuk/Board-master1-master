using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using MyBoard.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace MyBoard.Helpers
{
  public static class StorageHelper
  {
    
    public static bool IsImage(IFormFile file)
    {
      if (file.ContentType.Contains("image"))
      {
        return true;
      }

      string[] formats = { ".jpg", ".png", ".gif", ".jpeg" };

      return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
    }

    public static string GetRandomBlobName(string filename)
    {
      string ext = filename;
      return $"{Guid.NewGuid():N}{ext}";
    }

    public static async Task<string> UploadFileToStorage(Stream fileStream, string fileName,
                                                        AzureStorageConfig storageConfig)
    {
      var thumbnailWidth = 320;
      var extension = Path.GetExtension(fileName);
      var encoder = GetEncoder(extension);

      await using var output = new MemoryStream();
      using Image image = Image.Load(fileStream);
      var divisor = image.Width / thumbnailWidth;
      var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

      image.Mutate(x => x.Resize(thumbnailWidth, height));
      image.Save(output, encoder);
      output.Position = 0;

      string str = GetRandomBlobName(fileName);
      fileStream.Seek(0, SeekOrigin.Begin);

      Uri blobUri = new Uri("https://" +
                            storageConfig.AccountName +
                            ".blob.core.windows.net/" +
                            storageConfig.ImageContainer +
                            "/" + str);

      StorageSharedKeyCredential storageCredentials =
          new StorageSharedKeyCredential(storageConfig.AccountName, storageConfig.AccountKey);

      BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

      await blobClient.UploadAsync(fileStream);
      
      Uri blobUri1 = new Uri("https://" +
                            storageConfig.AccountName +
                            ".blob.core.windows.net/" +
                            storageConfig.ThumbnailContainer +
                            "/" + str);

      BlobClient blobClient1 = new BlobClient(blobUri1, storageCredentials);

      await blobClient1.UploadAsync(output);

      return str;
    }
    private static IImageEncoder GetEncoder(string extension)
    {
      IImageEncoder encoder = null;

      extension = extension.Replace(".", "");

      var isSupported = Regex.IsMatch(extension, "gif|png|jpe?g", RegexOptions.IgnoreCase);

      if (isSupported)
      {
        switch (extension)
        {
          case "png":
            encoder = new PngEncoder();
            break;
          case "jpg":
            encoder = new JpegEncoder();
            break;
          case "jpeg":
            encoder = new JpegEncoder();
            break;
          case "gif":
            encoder = new GifEncoder();
            break;
          default:
            break;
        }
      }

      return encoder;
    }

  }
}
