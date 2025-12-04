using System.Text.Json;
using ebay.domain.Entities;
using Microsoft.AspNetCore.Http;

public class CurrentUserService : ICurrentUserService
{
    public int UserId { get; }

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        var idString = accessor.HttpContext?.User.FindFirst("UserId")?.Value;

        if (int.TryParse(idString, out var parsedId))
        {
            UserId = parsedId;
        }
    }
}
