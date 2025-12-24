using ebay.application.Features.Users;
using ebay.application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender _sender) : ControllerBase
    {
        [HttpGet("get-me")]
        [Authorize]
        [UserIdClaimFilter]
        public async Task<IActionResult> GetMe()
        {
            var userId = HttpContext.Items["id"];
            var query = new GetMeQuery((int)userId!);
            var result = await _sender.Send(query);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPut("update-me")]
        [Authorize]
        [UserIdClaimFilter]
        public async Task<IActionResult> UpdateMe(UpdateMeDto dto)
        {
            var userId = HttpContext.Items["id"];
            var command = new UpdateMeCommand((int)userId!, dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }


        #region UPLOAD FILE
        [HttpPost("upload-avatar")]
        [Authorize]
        public async Task<IActionResult> UploadAvatar([FromForm] UploadAvatarDto dto)
        {
            var command = new UploadAvatarCommand(dto);
            var result = await _sender.Send(command);
            return result!.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }
        #endregion


        #region CHANGE PASSWORD
        [HttpPut("change-password")]
        [Authorize]
        [UserIdClaimFilter]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userId = HttpContext.Items["id"];
            var command = new ChangePasswordCommand((int)userId!, dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }
        #endregion

    }
}