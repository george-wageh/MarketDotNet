using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class CartService
    {
        public CartService(ICartRepository cartRepository, UnitWork.UnitWorkApp UnitWorkApp)
        {
            CartRepository = cartRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        public ICartRepository CartRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }


        public async Task<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>> GetAllProductsAsync(string userId )
        {
            var cartProducts = await this.CartRepository.GetCartProductsAsync(userId);
            var productsDTO = cartProducts.Select(x => new ProductQuantityCardDTO
            {
                Id = x.Product.Id,
                ImageUrl = x.Product.Image,
                Name = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity
            });
            return new ResponseDTO<IEnumerable<ProductQuantityCardDTO>>
            {
                Data = productsDTO,
                Message = "",
                Success = true
            };
        }

        public async Task<ResponseDTO<object>> AddToCart(string userId, int productId)
        {
            var cartProduct = await this.CartRepository.GetProductAsync(userId, productId);
            if (cartProduct == null)
            {
                cartProduct = new CartProduct { ProductId = productId, UserId = userId, Quantity = 1 };

                await this.CartRepository.AddToCartAsync(cartProduct);
            }
            else
            {
                cartProduct.Quantity += 1;
            }
            await UnitWorkApp.SaveChangesAsync();
            return new ResponseDTO<object>
            {
                Data = "",
                Message = "",
                Success = true
            };
        }
        public async Task<ResponseDTO<object>> ChanageQuantity(string userId, int productId , int newQuantity)
        {
            if (newQuantity > 0) {
                var cartProduct = await this.CartRepository.GetProductAsync(userId, productId);
                if (cartProduct == null)
                {
                    return new ResponseDTO<object>
                    {
                        Data = "",
                        Message = "Product not found in cart",
                        Success = false
                    };
                }
                cartProduct.Quantity = newQuantity;
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Data = "",
                    Message = "",
                    Success = true
                };
            }
            else {
              return new ResponseDTO<object>
              {
                  Data = "",
                  Message = "Quantity must greater than zero",
                  Success = false
              };
            }
     
        }



        public async Task<ResponseDTO<object>> RemoveProduct(string userId, int productId)
        {

            var cartProduct = await this.CartRepository.GetProductAsync(userId, productId);
            if (cartProduct != null)
            {
                this.CartRepository.Delete(cartProduct);
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Data = "",
                    Message = "",
                    Success = true
                };
            }
            else {
                return new ResponseDTO<object>
                {
                    Data = "",
                    Message = "Product not found",
                    Success = false
                };
            }

        }


    }
}
