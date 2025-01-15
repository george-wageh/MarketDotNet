using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class AddressController : ControllerBase
    {
        public AddressService AddressService { get; }

        // GET: api/<AddressController>
        public AddressController(AddressService addressService)
        {
            AddressService = addressService;
        }
        [HttpGet()]
        public async Task<ActionResult<ResponseDTO<IEnumerable<AddressDTO>>>> Get()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                var response = await AddressService.GetAllAsync(userId);
                if (response.Success)
                    return Ok(response);
            }
            return this.Unauthorized();
        }



        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<AddressDTO>>> Get(int id)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                var response = await AddressService.GetAsync(userId, id);
                if (response.Success)
                    return Ok(response);
            }
            return this.Unauthorized();
        }
        
        [HttpGet("GetDefaultAddress")]
        public async Task<ActionResult<ResponseDTO<AddressDTO>>> GetDefaultAddress()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                var response = await AddressService.GetDefaultAsync(userId);
                return Ok(response);

            }
            return this.Unauthorized();

        }
        // POST api/<AddressController>
        [HttpPost]
        public async Task<ActionResult<ResponseDTO<AddressDTO>>> AddAsync([FromBody] AddressDTO addressDTO)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userName != null)
                {
                    string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                    var response = await AddressService.AddAsync(userId, addressDTO);
                    return Ok(response);
                    //if (response.Success)
                }
                return this.Unauthorized();
            }
            else {
                return Ok(new ResponseDTO<AddressDTO>
                {
                    Success = false,
                    Data = null,
                    Message = ModelState.Aggregate("", (s, x) => s + $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}\n")
                });
            }
   
        }
        // POST api/<AddressController>
        [HttpPost("setDefault")]
        public async Task<ActionResult<ResponseDTO<object>>> SetDefaultAsync([FromBody] int addressId)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                var response = await AddressService.SetDefaultAsync(userId, addressId);
                return Ok(response);

            }
            return this.Unauthorized();
        }
        // PUT api/<AddressController>/5
        [HttpPut()]
        public async Task<ActionResult<ResponseDTO<AddressDTO>>>  Put([FromBody] AddressDTO addressDTO)
        {

            if (ModelState.IsValid)
            {
                var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userName != null)
                {
                    string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                    var response = await AddressService.EditAsync(userId, addressDTO);
                    return Ok(response);
                }
                return this.Unauthorized();
            }
            else
            {
                return Ok(new ResponseDTO<AddressDTO>
                {
                    Success = false,
                    Data = null,
                    Message = ModelState.Aggregate("", (s, x) => s + $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}\n")
                });
            }
         
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<object>>>  Delete(int id)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await AddressService.UnitWorkApp.GetUserId(userName.Value);
                var response = await AddressService.DeleteAsync(userId, id);
                return Ok(response);
            }
            return this.Unauthorized();
        }


    }
}
