using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.ViewModels
{
  public class AdvertEditViewModel : AdvertCreateViewModel
  {
    public int Id { get; set; }
    public string ExistingPhotoPath { get; set; }
  }
}
