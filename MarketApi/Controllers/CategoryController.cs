using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.AdminDTO;
using SharedLib.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(Roles ="Admin,User")]
    public class CategoryController : ControllerBase
    {
        public CategoryService CategoryService { get; }

        // GET: api/<CategoryController>
        public CategoryController(CategoryService categoryService)
        {
            CategoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO<IEnumerable<CategoryDTO>>>> Get()
        {
             return await CategoryService.GetAllAsync();
        }
        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> Post(CategoryDTO categoryDTO)
        {
            return Ok(await CategoryService.AddAsync(categoryDTO));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> Get(int id)
        {
            return await CategoryService.GetAsync(id);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> Put([FromRoute]int id, [FromBody] string name)
        {
            return Ok(await CategoryService.EditNameAsync(id, name));
        }
    
    }
}
