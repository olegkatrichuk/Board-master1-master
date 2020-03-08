using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
  public interface IFiles
  {
    void WorkWithFile(string path, IFormFile file);
  }
}
