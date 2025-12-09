using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ebay.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ISender _sender) : ControllerBase
    {
        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var query = new GetCategoriesQuery();
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