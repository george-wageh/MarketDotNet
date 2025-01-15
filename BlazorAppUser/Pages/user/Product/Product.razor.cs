using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Product
{
    public partial class Product
    {
        [Inject]
        public ProductService ProductService { get; set; }

        [Inject]
        public CartService CartService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public ProductDTO ProductDTO { get; set; }

        public ResponseDTO<object>? message { get; set; } = null;

        protected async override Task OnInitializedAsync()
        {

            //return base.OnInitializedAsync();
            var responseDTO = await ProductService.GetProductAsync(Id);
            if (responseDTO != null) {
                if (responseDTO.Success)
                {
                    ProductDTO = responseDTO.Data;
                }
            }
        }
        public async Task AddToCartAsync() {
            var response = await CartService.AddToCartAsync(Id);
            if (response != null)
            {
                if (response.Success)
                {
                    message = new ResponseDTO<object>()
                    {

                        Message = $"{ProductDTO.Name} added to cart",
                        Success = true,
                        Data = ""
                    };

                    // Trigger UI update
                    StateHasChanged();

                    await Task.Delay(3000);

                    message = null;

                    // Trigger UI update
                    StateHasChanged();
                }
            }
           
        }
    }
}
