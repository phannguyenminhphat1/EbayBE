using ebay.application.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ISender _sender) : ControllerBase
    {
        [HttpGet("get-products")]
        [PaginationFilter]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationDto paginationDto)
        {
            var query = new GetProductListCategoryQuery(paginationDto);
            var result = await _sender.Send(query);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }



        [HttpGet("get-list-listing-products-detail")]
        [PaginationFilter]
        public async Task<IActionResult> GetListListingProductDetail([FromQuery] PaginationDto paginationDto, [FromQuery] ListingProductDetailQueryDto listingProductDetailQueryDto)
        {
            var query = new GetListListingProductDetailQuery(paginationDto, listingProductDetailQueryDto);
            var result = await _sender.Send(query);
            return result.StatusCode switch
            {
                400 => BadRequest(result),
                404 => NotFound(result),
                422 => UnprocessableEntity(result),
                _ => Ok(result)
            };
        }

        [HttpGet("get-listing-product-detail/{id}")]
        [ParseIdFilter]
        public async Task<IActionResult> GetListingProductDetail([FromRoute] string id)
        {
            var query = new GetListingProductDetailByIdQuery(id);
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