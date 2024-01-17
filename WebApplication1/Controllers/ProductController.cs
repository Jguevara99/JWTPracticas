using JwtPractica.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var listproduct = ProductConstants.Products;

            return Ok(listproduct);
        }
    }
}
