using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Models;

namespace MyBoard.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Advert> Adverts { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
