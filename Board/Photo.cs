using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;

namespace MyBoard
{
    public class Photo
    {
        public void Construct(ConfigBuilder configBuilder)
        {
            configBuilder.SetUnicName();
            configBuilder.SetGetUrl();
            configBuilder.SetOutUrl();
        }

    }
}
