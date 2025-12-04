using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ebay.application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet("get-products")]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }
    }
}