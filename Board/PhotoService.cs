using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard
{
    class GetUrl
    {
        public string GetMyUrl { get; set; }
    }

    class OutUrl
    {
        public string OutMyUrl { get; set; }
    }

    class GetUnicName
    {
        public string Name { get; }
    }

    class Service
    {
        public GetUrl GetUrl { get; set; }
        public OutUrl OutUrl { get; set; }
        public GetUnicName GetUnicName { get; set; }

        public void GetResult()
        {

        }


    }

    abstract class PhotoServiceBilder
    {
        public Service Service { get;  private set; }

        public void CreatePhotoServiceBilder()
        {
            Service = new Service();
        }

        public abstract void SetGetUrl();
        public abstract void SetOutUrl();
        public abstract void SetUnicName();
    }

    class PhotoService
    {
        public Service Result(PhotoServiceBilder photoServiceBilder)
        {
            photoServiceBilder.SetUnicName();
            photoServiceBilder.SetGetUrl();
            photoServiceBilder.SetOutUrl();
            photoServiceBilder.CreatePhotoServiceBilder();

            return photoServiceBilder.Service;
        }
    }




    class OneBuilder : PhotoServiceBilder
    {
        public override void SetGetUrl()
        {
            
        }

        public override void SetOutUrl()
        {
            
        }

        public override void SetUnicName()
        {
            
        }
    }

    class TwoBilder : PhotoServiceBilder
    {
        public override void SetGetUrl()
        {
           
        }

        public override void SetOutUrl()
        {
           
        }

        public override void SetUnicName()
        {
            
        }
    }

}
