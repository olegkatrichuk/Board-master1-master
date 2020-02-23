using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.ViewModels
{
  public class UserClaimsViewModel
  {
    public UserClaimsViewModel()
    {
      Claims = new List<UserClaim>();
    }

    public string UserId { get; set; }
    public List<UserClaim> Claims { get; set; }
  }
}
