using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Cart
{
    public partial class SelectProducts
    {
        [Inject]
        public CartService CartService { get; set; }

        [Parameter]
        public IEnumerable<ProductQuantityCardDTO> Products { get; set; } = null;


        [Parameter]
        public EventCallback<IEnumerable<ProductQuantityCardDTO>> OnProductsChanged { get; set; }

        private async Task Delete(int productId) {
            var responseDTO = await CartService.DeleteAsync(productId);
            if (responseDTO.Success)
            {
                Products = Products.Where(x => x.Id != productId).ToList();
                StateHasChanged();
                await OnProductsChanged.InvokeAsync(Products);
            }
        }
        private async Task ChnageQuantity(int productId , object? value)
        {
            if (value != null) {
                int Quantity;
                int.TryParse((string?)value , out Quantity);
                var responseDTO = await CartService.ChnageQuantity(productId , Quantity);
                if (responseDTO.Success)
                {
                    var product = Products.Where(x => x.Id == productId).FirstOrDefault();
                    if (product != null)
                    {
                        product.Quantity = Quantity;
                    }
                    else
                    {
                        product.Quantity = 1;
                    }
                }
                StateHasChanged();
            }
         
        }
    }
}
