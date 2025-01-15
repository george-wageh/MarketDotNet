using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    //  RemoveProductFromFav
    [Route("api/favoriteProducts")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class FavoriteProductController : ControllerBase
    {
        public FavoriteProductService FavoriteProductServices { get; }

        // GET: api/<FavoriteProductController>

        public FavoriteProductController(FavoriteProductService favoriteProductServices)
        {
            FavoriteProductServices = favoriteProductServices;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ProductCardDTO>>>> Get()
        {

            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await FavoriteProductServices.UnitWorkApp.GetUserId(userName.Value);
                var response = await FavoriteProductServices.GetAllFavListAsync(userId);
                if (response.Success)
                    return Ok(response);

            }
            return this.Unauthorized();
 

        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<object>>> Post([FromBody] int productId)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await FavoriteProductServices.UnitWorkApp.GetUserId(userName.Value);
                var response = await FavoriteProductServices.AddProductToFav(userId, productId);
                if (response.Success)
                    return Ok(response);

            }
            return this.Unauthorized();

        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<ResponseDTO<object>>> Delete(int productId)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await FavoriteProductServices.UnitWorkApp.GetUserId(userName.Value);
                var response = await FavoriteProductServices.RemoveProductFromFav(userId, productId);
                if (response.Success)
                    return Ok(response);

            }
            return this.Unauthorized();
        }
    }
}
