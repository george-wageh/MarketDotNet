using MarketApi.Data;
using MarketApi.DTO;
using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SharedLib.DTO;
using System.Globalization;
using System.Linq;

namespace MarketApi.Services
{
    public class ProductService
    {
        public ProductService(IProductsRepository productsRepository,IFavoriteProductRepository favoriteProductRepository,ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment ,UnitWorkApp UnitWorkApp)
        {
            ProductsRepository = productsRepository;
            FavoriteProductRepository = favoriteProductRepository;
            CategoryRepository = categoryRepository;
            WebHostEnvironment = webHostEnvironment;
            this.UnitWorkApp = UnitWorkApp;
        }

        private IProductsRepository ProductsRepository { get; }
        public IFavoriteProductRepository FavoriteProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public UnitWorkApp UnitWorkApp { get; }


        public async Task<ResponseDTO<string>> UploadImage(IFormFile file) {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is invalid");

            string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images\\products");
            // Ensure the directory exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Create a unique file name
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Get the full file path
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the file to disk
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new ResponseDTO<string> {
                Success = true,
                Data = Path.Combine("sfroot\\images\\products", uniqueFileName).Replace("\\", "/"),
                Message = ""
            };

        }

        public async Task<ResponseDTO<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await ProductsRepository.GetByIdAsync(id);
            if (product != null)
            {

                var productDto = new ProductDTO
                {
                    Price = product.Price,
                    Description = product.Description,
                    Id = product.Id,
                    ImageUrl = product.Image,
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                };
                return new ResponseDTO<ProductDTO>
                {
                    Data = productDto,
                    Success = true,
                    Message = ""
                };
            }
            else
            {
                return new ResponseDTO<ProductDTO>
                {
                    Data = null,
                    Success = false,
                    Message = ""
                };
            }

        }


        public async Task<ResponseDTO<ProductDTO>> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl,
                Price = productDto.Price,
                Name = productDto.Name,
                CategoryId = productDto.CategoryId
            };
            await ProductsRepository.AddAsync(product);
            await UnitWorkApp.SaveChangesAsync();
            product.Id = product.Id;

            return new ResponseDTO<ProductDTO>
            {
                Success = true,
                Message = "",
                Data = productDto
            };
        }


        public async Task<ResponseDTO<object>> DeleteProductAsync(int id)
        {
            var product = await ProductsRepository.GetByIdAsync(id);
            if (product != null)
            {
                ProductsRepository.Delete(product);
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Success = true,
                    Message = "",
                    Data = ""
                };
            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = ""
                };
            }
        }


        public async Task<ResponseDTO<object>> EditProductAsync(ProductDTO productDTO)
        {
            var product = await ProductsRepository.GetByIdAsync(productDTO.Id);
            if (!(await CategoryRepository.IsExistAsync(productDTO.CategoryId))) {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Message = "Category not found",
                    Data = ""
                };
            }
            if (product != null)
            {
                {
                    product.Name = productDTO.Name;
                    product.Price = productDTO.Price;
                    product.Description = productDTO.Description;
                    product.ImageUrl = productDTO.ImageUrl=="" ? product.ImageUrl : productDTO.ImageUrl;
                    product.CategoryId = productDTO.CategoryId;
                    await UnitWorkApp.SaveChangesAsync();
                }
                return new ResponseDTO<object>
                {
                    Success = true,
                    Message = "",
                    Data = ""
                };
            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = ""
                };
            }
        }

        public async Task<ResponseListDTO<IEnumerable<ProductCardDTO>>> GetProductsInQueryAsync(string? userId , ProductQueryDTO productQueryDTO) {
            IQueryable<Product> combinedQuery = ProductsRepository.GetEmptyIQueryable();  // Start with all products

            // Apply category filtering if necessary
            if (productQueryDTO.CategoryId != null)
            {
                var allChildrenIds = await CategoryRepository.GetCategoryAndChildrenIds((int)productQueryDTO.CategoryId);
                foreach (var childId in allChildrenIds)
                {
                    var productsInCategory = ProductsRepository.GetAllInCategoryAsync(childId);
                    combinedQuery = combinedQuery.Union(productsInCategory);  // Combine results from multiple categories
                }
            }

            // Apply search query if necessary
            if (productQueryDTO.Qstring != null)
            {
                if (productQueryDTO.Qstring != "")
                {
                    combinedQuery = combinedQuery.Intersect(ProductsRepository.SearchInName(productQueryDTO.Qstring));  // Apply search filter
                }
            }
            bool SortBy = false;
            if (productQueryDTO.SortBy != null) {
                if (productQueryDTO.SortBy != "")
                {
                    if (productQueryDTO.SortBy == "PriceLowHigh")
                    {
                        combinedQuery = combinedQuery.OrderBy(x => x.Price);
                        SortBy = true;

                    }
                    else if (productQueryDTO.SortBy == "PriceHighLow")
                    {
                        combinedQuery = combinedQuery.OrderByDescending(x => x.Price);
                        SortBy = true;
                    }
                }
            }
            if (!SortBy) { 
               combinedQuery = combinedQuery.OrderByDescending(x => x.Id);
            }
            // Apply pagination
            if (productQueryDTO.Count == null)
            {
                productQueryDTO.Count = 5;  // Default to 5 if no Count specified
            }
            if (productQueryDTO.PageNum == null)
            {
                productQueryDTO.PageNum = 1;  // Default to first page if no PageNum specified
            }

            var Count = await combinedQuery.CountAsync();

            var skip = (productQueryDTO.PageNum.Value - 1) * productQueryDTO.Count.Value;

            // Get the results with pagination applied
            combinedQuery = combinedQuery
                                     .Skip(skip)  // Apply Skip and Take on IQueryable
                                     .Take(productQueryDTO.Count.Value);


            HashSet<int> idsFav= new HashSet<int>();
            if (userId != null) { 
                idsFav = FavoriteProductRepository.GetAllProductIdsFavQueryable(userId).ToHashSet();
            }

            var products = await combinedQuery.ToListAsync();


            // Map the results to ProductCardDTO
            var productsDTO = products.Select(x => new ProductCardDTO
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.Image,
                Price = x.Price,
                CategoryId = x.CategoryId,
                IsFav = idsFav.Contains(x.Id),
            }).ToList();

            // Return the results in a ResponseDTO
            return new ResponseListDTO<IEnumerable<ProductCardDTO>>
            {
                Data = productsDTO,
                Success = true,
                Message = "",
                Count = Count
            };

        }



    }
}
