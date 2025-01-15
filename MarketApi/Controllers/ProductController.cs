
using MarketApi.DTO;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ProductController : ControllerBase
    {
        public ProductService ProductService { get; }

        public ProductController(ProductService productService)
        {
            ProductService = productService;
        }
        // GET: api/<ProductController>
        [HttpPost("GetProductsInQuery")]
        public async Task<ActionResult<ResponseListDTO<IEnumerable<ProductDTO>>>> GetProductsInQueryAsync(ProductQueryDTO productQueryDTO)
        {
            //
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null) {
                string userId = await ProductService.UnitWorkApp.GetUserId(userName.Value);
                var respose = await ProductService.GetProductsInQueryAsync(userId, productQueryDTO);
                if (respose.Success)
                    return this.Ok(respose);
            }
            return this.Unauthorized();
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<ProductDTO>>> Get(int id)
        {
            var respose = await ProductService.GetByIdAsync(id);
            if (respose.Success)
                return this.Ok(respose);
            return this.BadRequest();
        }

    }
}
