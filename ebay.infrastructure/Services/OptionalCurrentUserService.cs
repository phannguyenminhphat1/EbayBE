using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class OptionalCurrentUserService : IOptionalCurrentUserService
{
    public int? UserId { get; }
    public bool IsAuthenticated => UserId.HasValue;

    public OptionalCurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = user.FindFirst("Id") ?? user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var id))
            {
                UserId = id;
            }
        }
    }
}
