using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/AdminPayments")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class AdminPaymentsController : ControllerBase
    {
        public AdminPaymentsController(PaymentService paymentService)
        {
            PaymentService = paymentService;
        }

        public PaymentService PaymentService { get; }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<PaymentCardDTO>>>> Get([FromRoute] string userName)
        {
            var userId = await PaymentService.UnitWorkApp.GetUserId(userName);
            if (userId == null)
            {
                return BadRequest();
            }
            var responseDTO = await PaymentService.GetAllAsync(userId);
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
