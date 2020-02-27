using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Board.Models;

namespace Board.ViewModels
{
  public class AdvertViewModel
  {
    public string Title { get; set; }
    
    public Categor? Category { get; set; }

    public ProductNew? ProductIsNew { get; set; }

    public decimal Price { get; set; }

    public bool IsNegotiatedPrice { get; set; }

    public string Description { get; set; }

    public string PhotoPath { get; set; }

    public DataType DataStart { get; set; }

    [Phone]
    public string Phone { get; set; }

    }
}
