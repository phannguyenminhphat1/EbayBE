using System.Text.Json;
using ebay.application.Features.Auth;
using ebay.application.Features.Auth.Commands.Login;
using ebay.application.Features.Auth.Commands.Logout;
using ebay.application.Features.Auth.Commands.RefreshToken;
using ebay.application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender _sender) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var command = new LoginCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var command = new RegisterCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto dto)
        {
            var command = new LogoutCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var command = new RefreshTokenCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpGet("oauth/google")]
        public async Task<IActionResult> OAuthGoogle()
        {
            var code = Request.Query["code"].ToString();
            if (string.IsNullOrEmpty(code))
                return BadRequest("Missing authorization code");
            var command = new LoginGoogleOAuthCommand(code);
            var result = await _sender.Send(command);
            if (result.StatusCode != 200 || result.Data == null)
                return Redirect("http://localhost:3001/login?error=oauth_failed");
            var accessToken = result.Data.AccessToken;
            var refreshToken = result.Data.RefreshToken;
            var redirectUrl = $"http://localhost:3001/login" + $"?access_token={Uri.EscapeDataString(accessToken)}" + $"&refresh_token={Uri.EscapeDataString(refreshToken)}";

            return Redirect(redirectUrl);


        }



    }
}