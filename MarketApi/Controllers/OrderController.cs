using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class OrderController : ControllerBase
    {
        public OrderService OrderService { get; }

        public OrderController(OrderService orderService)
        {
            OrderService = orderService;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseDTO<IEnumerable<OrderCardDTO>>>> Get()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await OrderService.UnitWorkApp.GetUserId(userName.Value);
                return Ok(await OrderService.GetAllAsync(userId));
            }
            return this.Unauthorized();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<OrderDTO>>> Get(int id)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await OrderService.UnitWorkApp.GetUserId(userName.Value);
                return Ok(await OrderService.GetOrderAsync(userId, id));

            }
            return this.Unauthorized();
        }


        [HttpPost]
        public async Task<ActionResult<ResponseDTO<object>>> Post()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await OrderService.UnitWorkApp.GetUserId(userName.Value);
                return Ok(await OrderService.PlaceOrder(userId));
            }
            return this.Unauthorized();
        }
    }
}
