using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class CategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository  , UnitWorkApp UnitWorkApp)
        {
            CategoryRepository = categoryRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        public ICategoryRepository CategoryRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }

        public async Task<ResponseDTO<CategoryDTO>> AddAsync(CategoryDTO categoryDTO) {
            if (categoryDTO.ParentId == null) {
                return new ResponseDTO<CategoryDTO>
                {
                    Success = false,
                    Data = categoryDTO,
                    Message = "Category parent not found"
                };
            }
            var ParentCategory = await CategoryRepository.GetAsync((int)categoryDTO.ParentId);
            if (ParentCategory == null) {
                return new ResponseDTO<CategoryDTO>
                {
                    Success = false,
                    Data = categoryDTO,
                    Message = "Category parent not found"
                };
            }
            var category = new Category {
                Name = categoryDTO.Name,
                ParentId = categoryDTO.ParentId
            };
            await CategoryRepository.AddAsync(category);
            await UnitWorkApp.SaveChangesAsync();
            categoryDTO.Id = category.Id;
            return new ResponseDTO<CategoryDTO>
            {
                Success = true,
                Data = categoryDTO,
                Message = ""
            };
        }
        public async Task<ResponseDTO<IEnumerable<CategoryDTO>>> GetAllAsync()
        {
            var categories =  await CategoryRepository.GetAllAsync();
            return new ResponseDTO<IEnumerable<CategoryDTO>>
            {
                Success = true,
                Message = "",
                Data = categories.Select(x=>new CategoryDTO {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                })
            };
        }
        public async Task<ResponseDTO<CategoryDTO>> GetAsync(int id)
        {
            var category = await CategoryRepository.GetAsync(id);
            if (category != null)
            {
                return new ResponseDTO<CategoryDTO>
                {
                    Success = true,
                    Message = "",
                    Data = new CategoryDTO
                    {
                        Id = category.Id,
                        Name = category.Name,
                        ParentId = category.ParentId
                    }
                };
            }
            else {
                return new ResponseDTO<CategoryDTO>
                {
                    Success = false,
                    Message = "Category not found",
                    Data = null
                };
            }          
        }

        public async Task<ResponseDTO<object>> EditNameAsync(int id , string newName)
        {
            var category = await CategoryRepository.GetAsync(id);
            if (category != null)
            {
                category.Name = newName;
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Success = true,
                    Message = "",
                    Data=""
                };
            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Message = "Category not found",
                    Data = null
                };
            }
        }

    }
}
