using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PaymentController : ControllerBase
    {
        public PaymentService PaymentService { get; }

        // GET: api/<AddressController>
        public PaymentController(PaymentService paymentService)
        {
            PaymentService = paymentService;
        }
        [HttpGet()]
        public async Task<ActionResult<ResponseDTO<IEnumerable<PaymentCardDTO>>>> Get()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await PaymentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await PaymentService.GetAllAsync(userId);
                if (response.Success)
                    return Ok(response);
            }
            return this.Unauthorized();
        }



        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<PaymentCardDTO>>> Get(int id)
        {

            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await PaymentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await PaymentService.GetAsync(userId, id);
                if (response.Success)
                    return Ok(response);
            }
            return this.Unauthorized();

        }

        [HttpGet("GetDefaultAddress")]
        public async Task<ActionResult<ResponseDTO<PaymentCardDTO>>> GetDefaultAddress()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await PaymentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await PaymentService.GetDefaultAsync(userId);
                return Ok(response);
            }
            return this.Unauthorized();


        }
        // POST api/<AddressController>
        [HttpPost]
        public async Task<ActionResult<ResponseDTO<PaymentCardDTO>>> AddAsync([FromBody] PaymentDTO paymentDTO)
        {

            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await PaymentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await PaymentService.AddPaymentCard(userId, paymentDTO);
                if (response.Success)
                    return Ok(response);
            }
            return this.Unauthorized();
        }
        // POST api/<AddressController>
        [HttpPost("setDefault")]
        public async Task<ActionResult<ResponseDTO<object>>> SetDefaultAsync([FromBody] int addressId)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await PaymentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await PaymentService.SetDefaultAsync(userId, addressId);
                if (response.Success)
                    return Ok(response);
            }
            return this.Unauthorized();
        }


        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<object>>> Delete(int id)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await PaymentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await PaymentService.DeletePaymentCard(userId, id);
                return Ok(response);
            }
            return this.Unauthorized();
        }


    }

}
