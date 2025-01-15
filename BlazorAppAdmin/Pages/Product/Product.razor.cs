using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Product
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

    }
}
