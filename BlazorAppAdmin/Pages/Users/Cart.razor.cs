using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users
{
    public partial class Cart
    {
        [Inject]
        public CartService CartService { get; set; }
        public IEnumerable<ProductQuantityCardDTO> Products { get; set; } = null;

        [Parameter]
        public string UserName { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var response = await CartService.GetAllProductsAsync(UserName);
            if (response != null) {
                if (response.Success) {
                    Products = response.Data;
                }
            }
         }
    }
}
