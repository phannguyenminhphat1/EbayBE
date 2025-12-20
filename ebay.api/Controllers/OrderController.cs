using ebay.application.Features.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(ISender _sender) : ControllerBase
    {
        [HttpPost("add-to-cart")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var command = new AddToCartCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpGet("get-orders")]
        [Authorize]
        [UserIdClaimFilter]
        [PaginationFilter]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQueryDto dto, [FromQuery] PaginationDto paginationDto)
        {
            var userId = HttpContext.Items["id"];
            var command = new GetOrdersQuery((int)userId!, dto, paginationDto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPost("delete-order-details")]
        [Authorize]
        [UserIdClaimFilter]
        public async Task<IActionResult> DeleteOrderDetail([FromBody] DeleteOrderDetailDto dto)
        {
            var userId = HttpContext.Items["id"];
            var command = new DeleteOrderDetailCommand((int)userId!, dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPut("update-order-detail")]
        [Authorize]
        [UserIdClaimFilter]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailDto dto)
        {
            var userId = HttpContext.Items["id"];
            var command = new UpdateOrderDetailCommand((int)userId!, dto);
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