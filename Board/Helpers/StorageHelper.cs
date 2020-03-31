using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using MyBoard.Models;

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
      string str = GetRandomBlobName(fileName);

      Uri blobUri = new Uri("https://" +
                            storageConfig.AccountName +
                            ".blob.core.windows.net/" +
                            storageConfig.ImageContainer +
                            "/" + str);

      StorageSharedKeyCredential storageCredentials =
          new StorageSharedKeyCredential(storageConfig.AccountName, storageConfig.AccountKey);

      BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

      await blobClient.UploadAsync(fileStream);
      fileStream.Seek(0, SeekOrigin.Begin);

      Uri blobUri1 = new Uri("https://" +
                            storageConfig.AccountName +
                            ".blob.core.windows.net/" +
                            storageConfig.ThumbnailContainer +
                            "/" + str);

      BlobClient blobClient1 = new BlobClient(blobUri1, storageCredentials);

      await blobClient1.UploadAsync(fileStream);

      return str;
    }

  }
}
