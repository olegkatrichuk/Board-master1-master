using System.Collections.Generic;
using Board.ViewModels;

namespace MyBoard.ViewModels
{
  public class AdvertEditViewModel : AdvertCreateViewModel
  {
    public int Id { get; set; }

    public string  ExistingPhotoPath { get; set; }

    public List<string> ImageStrings { get; set; }

    public IList<string> PhotoUrlToDelete { get; set; }
  }
}
