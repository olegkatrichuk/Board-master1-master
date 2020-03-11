using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Models;

namespace MyBoard.Models
{
    public class AdvertPhoto
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}
