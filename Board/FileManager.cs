using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBoard
{
    public class FileManager
    {
        public IFiles Files { get; set; }
        protected  string Paths { get; set; }

        public FileManager(IFiles files,string path)
        {
            Files = files;
            Paths = path;
        }

        public void WorkWithFile()
        {
            Files.WorkWithFile();
        }

        public class DeleteMyFile:IFiles
        {
            public void WorkWithFile()
            {
                
            }
        }
        public class CreateMyFile:IFiles
        {
            public void WorkWithFile()
            {
                throw new NotImplementedException();
            }
        }
    }
}
