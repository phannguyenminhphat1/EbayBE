using ebay.application.Features.Orders;
using ebay.application.Features.Orders.Commands;
using ebay.application.Features.Orders.Queries;
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
            var query = new GetOrdersQuery(dto, paginationDto);
            var result = await _sender.Send(query);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }


        [HttpGet("get-orders-by-seller")]
        [Authorize(Roles = "Seller")]
        [PaginationFilter]
        public async Task<IActionResult> GetOrdersBySeller([FromQuery] GetOrdersQueryDto dto, [FromQuery] PaginationDto paginationDto)
        {
            var query = new GetOrdersBySellerQuery(dto, paginationDto);
            var result = await _sender.Send(query);
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


        [HttpPut("cancel-order/{id}")]
        [Authorize]
        [ParseIdFilter]
        public async Task<IActionResult> CancelOrder([FromRoute] string id)
        {
            var command = new CancelOrderCommand(id);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpPut("reject-confirm-order/{id}")]
        [Authorize(Roles = "Seller")]
        [ParseIdFilter]
        public async Task<IActionResult> RejectAndConfirmOrder([FromRoute] string id, [FromBody] RejectAndConfirmDto dto)
        {
            var command = new RejectAndConfirmOrderCommand(id, dto);
            var result = await _sender.Send(command);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }


        [HttpGet("get-order-statistics")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderStatistics([FromQuery] OrderStatisticsQueryDto dto)
        {
            var query = new GetOrderStatisticsQuery(dto);
            var result = await _sender.Send(query);
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