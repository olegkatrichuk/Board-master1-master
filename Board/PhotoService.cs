using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBoard;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;

namespace MyBoard
{

    public class ServiceConfig
    {
        public string GetUrl { get; set; }
        public string OutUrl { get; set; }
        public string GetUnicName { get; set; }
       
    }

    public abstract class ConfigBuilder
    {
        public abstract void SetGetUrl();
        public abstract void SetOutUrl();
        public abstract void SetUnicName();
        public abstract ServiceConfig GetResult();

    }

    class LocalServiceConfigBuilder : ConfigBuilder
    {
        readonly ServiceConfig _serviceConfig = new ServiceConfig();

        public override void SetGetUrl()
        {
            _serviceConfig.GetUrl = @"D:\Example\BoardLast\Board\wwwroot\images\";
        }
        public override void SetUnicName()
        {
            _serviceConfig.GetUnicName = $"{System.Guid.NewGuid()}.zip";
        }
        public override void SetOutUrl()
        {
            _serviceConfig.OutUrl = @"D:\" + _serviceConfig.GetUnicName;
        }
        public override ServiceConfig GetResult()
        {
            return _serviceConfig;
        }

    }
}

class TwoBilder : ConfigBuilder
{
    readonly ServiceConfig _serviceConfig = new ServiceConfig();
    public override void SetGetUrl()
    {
        _serviceConfig.GetUrl = @"D:\Example\BoardLast\Board\wwwroot\picture\";
    }

    public override void SetOutUrl()
    {
        _serviceConfig.OutUrl = @"D:\" + _serviceConfig.GetUnicName;
    }

    public override void SetUnicName()
    {
        _serviceConfig.GetUnicName = $"{System.Guid.NewGuid()}.zip";
    }

    public override ServiceConfig GetResult()
    {
        return _serviceConfig;
    }
}


