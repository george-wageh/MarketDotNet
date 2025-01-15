using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminProductController : ControllerBase
    {

        public ProductService ProductService { get; }

        public AdminProductController(ProductService productService)
        {
            ProductService = productService;
        }

        [HttpPost("GetProductsInQuery")]
        public async Task<ActionResult<ResponseListDTO<IEnumerable<ProductDTO>>>> GetProductsInQueryAsync(ProductQueryDTO productQueryDTO)
        {
            var respose = await ProductService.GetProductsInQueryAsync(null, productQueryDTO);
            return this.Ok(respose);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<ProductDTO>>> Get(int id)
        {
            var respose = await ProductService.GetByIdAsync(id);
            return this.Ok(respose);

        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ResponseDTO<ProductDTO>>> Post([FromBody] ProductDTO product)
        {

            var respose = await ProductService.AddProductAsync(product);
            return this.Ok(respose);
        }
        [HttpPost("PostImage")]
        public async Task<ActionResult<ResponseDTO<string>>> PostImage()
        {
            var image = Request.Form.Files[0];
            var respose = await ProductService.UploadImage(image);
            return this.Ok(respose);
        }
        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<ActionResult<ResponseDTO<object>>> Put([FromBody] ProductDTO product)
        {
            var respose = await ProductService.EditProductAsync(product);
            return this.Ok(respose);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<object>>> Delete(int id)
        {
            var respose = await ProductService.DeleteProductAsync(id);
            return this.Ok(respose);

        }
    }
}
