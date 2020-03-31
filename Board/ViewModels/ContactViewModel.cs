using System.ComponentModel.DataAnnotations;

namespace MyBoard.ViewModels
{
  public class ContactViewModel
  {
    [Required]
    [StringLength(60, MinimumLength = 5)]
    public string Name { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Subject { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string Message { get; set; }
  }
}
