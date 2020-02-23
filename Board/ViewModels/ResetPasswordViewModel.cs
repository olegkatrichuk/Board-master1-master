using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.ViewModels
{
  public class ResetPasswordViewModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public  string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password",ErrorMessage = "Password and Confirm Password must match")]
    public string ConfirmPassword { get; set; }

    public string Token { get; set; }

  }
}
