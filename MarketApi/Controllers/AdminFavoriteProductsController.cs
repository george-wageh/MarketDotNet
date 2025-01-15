using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminFavoriteProductsController : ControllerBase
    {
        public AdminFavoriteProductsController(FavoriteProductService favoriteProductService)
        {
            FavoriteProductService = favoriteProductService;
        }

        public FavoriteProductService FavoriteProductService { get; }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ProductCardDTO>>>> Get([FromRoute] string userName)
        {

            var UserId = await FavoriteProductService.UnitWorkApp.GetUserId(userName);
            if (UserId == null)
            {
                return BadRequest();
            }
            return Ok(await FavoriteProductService.GetAllFavListAsync(UserId));

        }
    }
}
