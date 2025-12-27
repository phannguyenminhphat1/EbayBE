using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class UserIdClaimFilter : ActionFilterAttribute
{
    private const string USER_ID_KEY = "id";
    private const string ROLES_KEY = "roles";


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                status = 401,
                message = "Unauthorized"
            });
            return;
        }
        var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        context.HttpContext.Items[USER_ID_KEY] = userId;
        context.HttpContext.Items[ROLES_KEY] = roles;
    }
}
