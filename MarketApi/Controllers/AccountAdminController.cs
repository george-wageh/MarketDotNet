using MarketApi.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAdminController : ControllerBase
    {
        public AccountAdminService AccountService { get; }

        public AccountAdminController(AccountAdminService accountService)
        {
            AccountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO<object>>> Register(UserRegisterDTO userRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await AccountService.Register(userRegisterDTO);
                return Ok(result);
            }
            else {
                return Ok(new ResponseDTO<object>
                {
                    Success = false,
                    Data = ModelState.Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value?.Errors.Select(y => y.ErrorMessage).ToList()
                    }),
                    Message = ModelState.Aggregate("",(s , x)=> s + $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}\n")
                });
            }
        }


        // POST api/<AccountController>
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO<object>>> Login(UserLoginDTO UserLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await AccountService.Login(UserLoginDTO);
                return Ok(result);
            }
            else
            {
                return Ok(new ResponseDTO<object>
                {
                    Success = false,
                    Data = ModelState.Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value?.Errors.Select(y => y.ErrorMessage).ToList()
                    }),
                    Message = ModelState.Aggregate("", (s, x) => s + $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}\n")
                });
            }
           
        }
    }
}
