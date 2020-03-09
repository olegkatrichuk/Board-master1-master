using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
    public interface IFileManager
    {
        string UploadFile(string path, IFormFile file);

        void DeleteFile(string path);
    }
}
