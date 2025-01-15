using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.AdminDTO;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        public UserService UserService { get; }

        public UsersController(UserService userService)
        {
            UserService = userService;
        }
        // GET: api/<UsersController>
        [HttpPost("GetUsers")]
        public async Task<ActionResult<ResponseListDTO<IEnumerable<UserDTO>>>> GetUsers(UserQueryDTO userQueryDTO)
        {
            return await UserService.GetUsers(userQueryDTO);
        }
    }
}
