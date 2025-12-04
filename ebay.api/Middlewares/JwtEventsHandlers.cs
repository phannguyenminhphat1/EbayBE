using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class JwtEventsHandlers
{
    public static async Task HandleJwtChallenge(JwtBearerChallengeContext context)
    {
        context.HandleResponse();
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";

        string message;

        if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
        {
            message = AuthMessages.ACCESS_TOKEN_IS_REQUIRED;
        }
        else if (context.AuthenticateFailure is SecurityTokenExpiredException)
        {
            message = AuthMessages.TOKEN_EXPIRED_ERROR;
        }
        else if (context.AuthenticateFailure is SecurityTokenValidationException ||
         context.AuthenticateFailure is SecurityTokenMalformedException ||
         context.AuthenticateFailure is ArgumentException)
        {
            message = AuthMessages.ACCESS_TOKEN_IS_INVALID;
        }
        else
        {
            message = context.AuthenticateFailure?.Message!;
        }
        var result = JsonSerializer.Serialize(new { message });
        await context.Response.WriteAsync(result);
    }
}
