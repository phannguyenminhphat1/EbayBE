using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ParseIdFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.ContainsKey("id"))
        {
            base.OnActionExecuting(context);
            return;
        }

        var rawId = context.ActionArguments["id"]?.ToString();

        if (string.IsNullOrWhiteSpace(rawId))
        {
            context.Result = new BadRequestObjectResult(new
            {
                status = 400,
                message = "Id is required"
            });
            return;
        }

        if (!int.TryParse(rawId, out var parsedId))
        {
            context.Result = new BadRequestObjectResult(new
            {
                status = 400,
                message = "Id must be a number"
            });
            return;
        }

        context.ActionArguments["id"] = parsedId.ToString();
        base.OnActionExecuting(context);
    }
}
