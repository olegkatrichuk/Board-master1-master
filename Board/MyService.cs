using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;

namespace MyBoard
{
    public class MyService
    {
        private readonly ServiceConfig _serviceConfigServiceConfig;
        public MyService(ServiceConfig serviceConfigServiceConfig)
        {
            _serviceConfigServiceConfig = serviceConfigServiceConfig;
        }
        
        public void CreateBackup()
        {
            ZipFile.CreateFromDirectory(_serviceConfigServiceConfig.GetUrl, _serviceConfigServiceConfig.OutUrl);
        }
    }
}
