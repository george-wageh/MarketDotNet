using MarketApi.Data;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class FavoriteProductService
    {
        public FavoriteProductService(IFavoriteProductRepository favoriteProductRepository , UnitWorkApp UnitWorkApp)
        {
            FavoriteProductRepository = favoriteProductRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        public IFavoriteProductRepository FavoriteProductRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }

        public async Task<ResponseDTO<IEnumerable<ProductCardDTO>>> GetAllFavListAsync(string userId) {
           var products = await FavoriteProductRepository.GetAllFavListAsync(userId);
            var ProductsDTO = products.Select(x => new ProductCardDTO
            {
                Price = x.Price,
                Name = x.Name,
                Id = x.Id,
                ImageUrl = x.Image,
                IsFav = true

            }).ToList();
            return new ResponseDTO<IEnumerable<ProductCardDTO>>
            {
                Data = ProductsDTO,
                Message = "",
                Success = true
            };
        }

        public async Task<ResponseDTO<object>> AddProductToFav(string userId , int productId) {
            if (!(await FavoriteProductRepository.IsProductFavoritedAsync(userId, productId)))
            {
                await FavoriteProductRepository.AddToFavorite(productId, userId);
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Message = "",
                    Data = "",
                    Success = true
                };
            }
            else {
                return new ResponseDTO<object>
                {
                    Message = "this product already found in favorite list",
                    Success = false,
                    Data = ""
                };
            }
        }

        public async Task<ResponseDTO<object>> RemoveProductFromFav(string userId , int productId)
        {
            if ((await FavoriteProductRepository.IsProductFavoritedAsync(userId, productId)))
            {
                FavoriteProductRepository.Remove(productId, userId);
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Message = "",
                    Success = true,
                    Data = ""
                };
            }
            else {
                return new ResponseDTO<object>
                {
                    Message = "this product not found in favorite list",
                    Success = false,
                    Data = ""
                };
            }
        }


    }
}
