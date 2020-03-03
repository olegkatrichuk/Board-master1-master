using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;

namespace MyBoard
{
    public class MyService
    {

        public void Show()
        {
            Photo ser = new Photo();

            PhotoServiceBilder b1 = new OneBuilder();
            ser.Construct(b1);
            Product p = b1.GetResult();
            ZipFile.CreateFromDirectory(p.GetUrl, p.OutUrl);

            PhotoServiceBilder b2 = new TwoBilder();
            ser.Construct(b2);
            Product p2 = b2.GetResult();
            ZipFile.CreateFromDirectory(p2.GetUrl, p2.OutUrl);
        }
    }
}
