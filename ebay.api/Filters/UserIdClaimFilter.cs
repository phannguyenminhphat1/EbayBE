using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class UserIdClaimFilter : ActionFilterAttribute
{
    private const string USER_ID_KEY = "id";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                status = 401,
                message = "Unauthorized"
            });
            return;
        }

        context.HttpContext.Items[USER_ID_KEY] = userId;
    }
}
