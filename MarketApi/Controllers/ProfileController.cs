using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize(Roles ="Admin,User")]
    public class ProfileController : ControllerBase
    {
        // GET: api/<ProfileController>
        public ProfileController(ProfileService profileService)
        {
            ProfileService = profileService;
        }

        public ProfileService ProfileService { get; }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO<ProfileDTO>>> Get()
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await ProfileService.UnitWorkApp.GetUserId(userName.Value);
                var response = await ProfileService.GetAsync(userId);
                return Ok(response);

            }
            return this.Unauthorized();
        }
    }
}
