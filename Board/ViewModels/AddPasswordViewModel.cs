using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.ViewModels
{
  public class AddPasswordViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "NewPassword")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new Password")]
    [Compare("NewPassword", ErrorMessage =
      "The new password and confirmation password do not much.")]
    public string ConfirmPassword { get; set; }
  }
}
