using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Api.Filters;

public class BindFromClaimsAttribute : Attribute, IAsyncActionFilter
{
    public string Claim { get; set; }
    public string ParameterName { get; set; }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        if (user.Identity.IsAuthenticated)
        {
            var claim = user.FindFirstValue(Claim);

            if (!string.IsNullOrEmpty(claim) &&
                !string.IsNullOrEmpty(ParameterName))
            {
                context.ActionArguments[ParameterName] = claim;
            }
        }
        await next();
    }
}
