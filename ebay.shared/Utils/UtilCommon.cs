using System.Security.Claims;

public static class UtilCommon
{
    public static Guid GetUserIdFromHeader(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException(message: AuthMessages.ACCESS_TOKEN_IS_REQUIRED);
        }

        return userId;
    }

}