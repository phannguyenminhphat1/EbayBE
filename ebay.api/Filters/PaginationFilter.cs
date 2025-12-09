using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class PaginationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue("paginationDto", out var obj) || obj is not PaginationDto dto)
        {
            base.OnActionExecuting(context);
            return;
        }

        var pageStr = dto.Page;
        var pageSizeStr = dto.PageSize;

        // Default
        int page = 1;
        int pageSize = 10;

        // Validate page
        if (!string.IsNullOrWhiteSpace(pageStr))
        {
            if (!int.TryParse(pageStr, out page) || page <= 0)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    status = 400,
                    message = "Invalid 'page'. It must be a positive integer."
                });
                return;
            }
        }

        // Validate page_size
        if (!string.IsNullOrWhiteSpace(pageSizeStr))
        {
            if (!int.TryParse(pageSizeStr, out pageSize) || pageSize <= 0)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    status = 400,
                    message = "Invalid 'page_size'. It must be a positive integer."
                });
                return;
            }
        }

        dto.Page = page.ToString();
        dto.PageSize = pageSize.ToString();
    }
}
