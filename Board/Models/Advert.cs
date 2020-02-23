using System.Collections;
using System.ComponentModel.DataAnnotations;
using MyBoard.Models;

namespace Board.Models
{
  public class Advert
  {
    public int Id { get; set; }

    [Required]
    [Display(Name = "Заголовок")]
    public string Title { get; set; }

    [Display(Name = "Категория")]
    public Categor? Category { get; set; }

    [Display(Name = "Новый б/у")]
    public ProductNew? ProductIsNew { get; set; }

    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Display(Name = "Договорная цена")]
    public bool IsNegotiatedPrice { get; set; }

    [Display(Name = "Описание")]
    public string Description { get; set; }

    [Display(Name = "Фотография")]
    public string PhotoPath { get; set; }

    public User User { get; set; }

    public string UserId { get; set; }
  }
}
