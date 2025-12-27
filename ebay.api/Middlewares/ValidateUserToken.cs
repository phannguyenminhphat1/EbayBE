using System.Security.Claims;
using System.Text.Json;
using ebay.domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public static class ValidateUserToken
{
    public static async Task HandleValidateUserToken(TokenValidatedContext context)
    {
        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var userId = int.Parse(context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await userRepository.FindUserById(userId);
        if (user == null || user.Deleted!.Value)
        {
            context.Fail(AuthMessages.USER_NOT_FOUND_OR_IS_DELETED);
            return;
        }
    }
}
