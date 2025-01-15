using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CartController : ControllerBase
    {
        public CartService CartService { get; }

        public CartController(CartService cartService)
        {
            CartService = cartService;
        }
        [HttpGet("products")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>>> Get()
        {

            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await CartService.UnitWorkApp.GetUserId(userName.Value);
                var response = await CartService.GetAllProductsAsync(userId);
                return Ok(response);

            }
            return this.Unauthorized();
        }

        // POST api/<CartController>
        [HttpPost("{productId}")]
        public async Task<ActionResult<ResponseDTO<object>>> AddToCart(int productId)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await CartService.UnitWorkApp.GetUserId(userName.Value);
                var response = await CartService.AddToCart(userId, productId);
                if (response.Success)
                {
                    return Ok(response);
                }
            }
            return this.Unauthorized();
           
        }

        // POST api/<CartController>
        [HttpPost("quantity/{productId}")]
        public async Task<ActionResult<ResponseDTO<object>>> ChangeQuantity([FromRoute] int productId , [FromBody] int quantity)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await CartService.UnitWorkApp.GetUserId(userName.Value);
                var response = await CartService.ChanageQuantity(userId, productId, quantity);
                if (response.Success)
                {
                    return Ok(response);
                }
                else return BadRequest(response);
            }
            return this.Unauthorized();
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ResponseDTO<object>>> Delete(int productId)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await CartService.UnitWorkApp.GetUserId(userName.Value);
                var response = await CartService.RemoveProduct(userId, productId);
                if (response.Success)
                {
                    return Ok(response);
                }
                else return BadRequest(response);
            }
            return this.Unauthorized();

        }
    }
}
