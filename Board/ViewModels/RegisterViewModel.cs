using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Board.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [EmailAddress]
    [Remote(action: "IsEmailInUse", "Account")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name="Повторите пароль")]
    [Compare("Password",
      ErrorMessage = "Пароли не совпадают.")]
    public string ConfirmPassword { get; set; }
  }
}
