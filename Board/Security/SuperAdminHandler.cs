﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Board.Security
{
  public class SuperAdminHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
      ManageAdminRolesAndClaimsRequirement requirement)
    {
      if (context.User.IsInRole("Super Admin"))
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
