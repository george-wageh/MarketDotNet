using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/adminOrders")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class AdminOrdersController : ControllerBase
    {
        public OrderService OrderService { get; }

        public AdminOrdersController(OrderService orderService)
        {
            OrderService = orderService;
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<OrderCardDTO>>>> GetAll([FromRoute] string userName)
        {
            var UserId = await OrderService.UnitWorkApp.GetUserId(userName);
            if (UserId == null)
            {
                return BadRequest();
            }
            return Ok(await OrderService.GetAllAsync(UserId));
        }


        [HttpGet("{userName}/{id}")]
        public async Task<ActionResult<ResponseDTO<OrderDTO>>> Get([FromRoute] string userName, [FromRoute] int id)
        {
            var UserId = await OrderService.UnitWorkApp.GetUserId(userName);
            if (UserId == null)
            {
                return BadRequest();
            }
            return Ok(await OrderService.GetOrderAsync(UserId, id));
        }

    }
}
