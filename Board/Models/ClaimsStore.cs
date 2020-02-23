using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Board.Models
{
  public static class ClaimsStore
  {
    public static List<Claim> AllClaims = new List<Claim>()
    {
      new Claim("Create role","Create role"),
      new Claim("Edit role","Edit role"),
      new Claim("Delete role","Delete role")
    };
  }
}
