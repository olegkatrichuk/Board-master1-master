using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBoard;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;

namespace MyBoard
{

    public class Product
    {
        public string GetUrl { get; set; }
        public string OutUrl { get; set; }
        public string GetUnicName { get; set; }

    }

    public abstract class PhotoServiceBilder
    {
        public abstract void SetGetUrl();
        public abstract void SetOutUrl();
        public abstract void SetUnicName();
        public abstract Product GetResult();

    }

    class OneBuilder : PhotoServiceBilder
    {
        readonly Product _product = new Product();

        public override void SetGetUrl()
        {
            _product.GetUrl = @"D:\Example\BoardLast\Board\wwwroot\images\";
        }
        public override void SetUnicName()
        {
            _product.GetUnicName = $"{System.Guid.NewGuid()}.zip";
        }
        public override void SetOutUrl()
        {
            _product.OutUrl = @"D:\" + _product.GetUnicName;
        }
        public override Product GetResult()
        {
            return _product;
        }

    }
}

class TwoBilder : PhotoServiceBilder
{
    readonly Product _product = new Product();
    public override void SetGetUrl()
    {
        _product.GetUrl = @"D:\Example\BoardLast\Board\wwwroot\picture\";
    }

    public override void SetOutUrl()
    {
        _product.OutUrl = @"D:\" + _product.GetUnicName;
    }

    public override void SetUnicName()
    {
        _product.GetUnicName = $"{System.Guid.NewGuid()}.zip";
    }

    public override Product GetResult()
    {
        return _product;
    }
}


