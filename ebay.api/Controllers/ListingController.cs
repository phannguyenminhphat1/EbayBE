using ebay.application.Features.Listing;
using ebay.application.Features.Listing.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController(ISender _sender) : ControllerBase
    {

        [HttpGet("get-listings")]
        [Authorize(Roles = "Admin,Seller")]
        [PaginationFilter]
        public async Task<IActionResult> GetListings([FromQuery] GetListingsQueryDto dto, [FromQuery] PaginationDto paginationDto)
        {
            var query = new GetListingsQuery(dto, paginationDto);
            var result = await _sender.Send(query);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPost("create-post")]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
        {
            var command = new CreatePostCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        #region UPLOAD IMAGES
        [HttpPost("upload-images")]
        [Authorize]
        public async Task<IActionResult> UploadImages([FromForm] UploadImagesDto dto)
        {
            var command = new UploadImagesCommand(dto);
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

    }
}