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
        [PaginationFilter]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQueryDto dto, [FromQuery] PaginationDto paginationDto)
        {
            var command = new GetOrdersQuery(dto, paginationDto);
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
        public async Task<IActionResult> DeleteOrderDetail([FromBody] DeleteOrderDetailDto dto)
        {
            var command = new DeleteOrderDetailCommand(dto);
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
        public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailDto dto)
        {
            var command = new UpdateOrderDetailCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }


        [HttpPost("buy-products")]
        [Authorize]
        public async Task<IActionResult> BuyProducts([FromBody] BuyProductsDto dto)
        {
            var command = new BuyProductsCommand(dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }


        [HttpPut("cancel-order")]
        [Authorize]
        public async Task<IActionResult> CancelOrder()
        {
            var command = new CancelOrderCommand();
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