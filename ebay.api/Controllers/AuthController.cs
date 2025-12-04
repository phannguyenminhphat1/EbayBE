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
        [Authorize]
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


    }
}