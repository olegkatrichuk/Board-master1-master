using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;

namespace MyBoard
{
    public class Photo
    {
        public void Construct(PhotoServiceBilder photoServiceBilder)
        {
            photoServiceBilder.SetUnicName();
            photoServiceBilder.SetGetUrl();
            photoServiceBilder.SetOutUrl();
        }

    }
}
