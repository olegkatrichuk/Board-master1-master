using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
