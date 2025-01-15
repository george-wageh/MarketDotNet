using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public CommentService CommentService { get; }

        public CommentController(CommentService commentService)
        {
            CommentService = commentService;
        }
        [HttpGet("{productId}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<CommentDTO>>>> GetAll(int productId)
        {
            var response = await CommentService.GetAllAsync(productId);
            return Ok(response);
        }

        // POST api/<CommentController>
        [HttpPost("{productId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ResponseDTO<CommentDTO>>> Post([FromQuery] int productId, [FromBody] CommentDTO commentDTO)
        {
            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await CommentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await CommentService.AddAsync(productId, userId, commentDTO);
                return Ok(response);

            }
            return this.Unauthorized();


        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ResponseDTO<object>>> Delete(int id)
        {

            var userName = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                string userId = await CommentService.UnitWorkApp.GetUserId(userName.Value);
                var response = await CommentService.DeleteAsync(userId, id);
                return Ok(response);

            }
            return this.Unauthorized();
        }
    }
}
