using System.ComponentModel.DataAnnotations;
using Board.Models;
using MyBoard.Models;

namespace MyBoard.ViewModels
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

    public City? City { get; set; }
    }
}
