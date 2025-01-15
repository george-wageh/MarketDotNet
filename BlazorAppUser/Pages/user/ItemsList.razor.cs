using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user
{
    public partial class ItemsList
    {

        [Inject]
        public CartService CartService { get; set; }
        [Inject]
        public FavoriteProductService FavoriteProductService { get; set; }
        [Parameter]
        public IEnumerable<ProductCardDTO> products { get; set; }

        [Parameter]
        public EventCallback<int> EventAddToCart { get; set; }
        [Parameter]
        public EventCallback<int> EventAddProductToFav { get; set; }
        private async Task AddToCart(int productId)
        {
            var x = await CartService.AddToCartAsync(productId);
            if (x != null) {
                if (x.Success)
                {
                    await EventAddToCart.InvokeAsync(productId);
                }
            }
        }

        private async Task ToggleFav(int productId)
        {
            var product = products.FirstOrDefault(x => x.Id == productId);
            if (product != null) {
                if (product.IsFav)
                {
                    var x = await FavoriteProductService.RemoveProductFromFav(productId);
                    if (x != null)
                    {
                        if (x.Success)
                        {
                            await EventAddProductToFav.InvokeAsync(productId);
                        }
                    }
                }
                else if(!product.IsFav)  {
                    var x = await FavoriteProductService.AddProductToFav(productId);
                    if (x != null)
                    {
                        if (x.Success)
                        {
                            await EventAddProductToFav.InvokeAsync(productId);
                        }
                    }
                }
               
            }
          
        }
    }
}
