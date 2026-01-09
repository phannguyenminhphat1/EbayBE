using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class CurrentUserService : ICurrentUserService
{
    public int UserId { get; }

    public List<string> Roles { get; }

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User ?? throw new UnauthorizedAccessException();

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            throw new UnauthorizedAccessException("Invalid user");

        UserId = userId;

        Roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
    }
}
