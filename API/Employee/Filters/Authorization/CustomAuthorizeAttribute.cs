using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;

namespace Employee.API.Filters.Authorization
{
    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string Role { get; set; }

        public CustomAuthorizeAttribute(string role)
        {
            Role = role;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Not authenticated" }));
                context.Result = new JsonResult(new { error = "Not authenticated" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var hasRole = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals(Role, StringComparison.OrdinalIgnoreCase));
            if (!hasRole)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Not authorized" }));
                context.Result = new JsonResult(new { error = "Not authorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                return;
            }
        }
    }
}
