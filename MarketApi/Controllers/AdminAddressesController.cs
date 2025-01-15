using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/AdminAddresses")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminAddressesController : ControllerBase
    {
        // GET: api/<AdminAddressesController>
        public AdminAddressesController(AddressService addressService)
        {
            AddressService = addressService;
        }

        public AddressService AddressService { get; }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<AddressDTO>>>> Get([FromRoute] string userName)
        {
            var userId = await AddressService.UnitWorkApp.GetUserId(userName);
            if (userId == null)
            {
                return BadRequest();
            }
            var responseDTO = await AddressService.GetAllAsync(userId);
            if (responseDTO != null)
            {
                return Ok(responseDTO);
            }
            else
            {
                return BadRequest();

            }
        }

       
    }
}
