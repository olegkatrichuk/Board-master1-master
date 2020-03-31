using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyBoard.Security
{
  public class SuperAdminHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
      ManageAdminRolesAndClaimsRequirement requirement)
    {
      if (context.User.IsInRole("Admin"))
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
