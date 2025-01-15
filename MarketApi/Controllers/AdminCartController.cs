using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/AdminCart")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminCartController : ControllerBase
    {
        // GET: api/<AdminCartController>
        public AdminCartController(CartService cartService)
        {
            CartService = cartService;
        }

        public CartService CartService { get; }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>>> Get([FromRoute] string userName)
        {
            var userId = await CartService.UnitWorkApp.GetUserId(userName);
            if (userId == null) { 
                return BadRequest();
            }
            var responseDTO =  await CartService.GetAllProductsAsync(userId);
            if (responseDTO != null)
            {
                return Ok(responseDTO);
            }
            else { 
                return BadRequest();

            }
        }
 
    }
}
