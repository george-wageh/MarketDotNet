using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users
{
    public partial class FavoriteProducts
    {
        public IEnumerable<ProductCardDTO> Products { get; set; }

        private List<ResponseDTO<object>> Messages { get; } = new();

        [Inject]
        public FavoriteProductService service { get; set; }

        [Parameter]
        public string userName { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //return base.OnInitializedAsync();
            var response = await service.GetAllFavListAsync(userName);
            if (response != null)
            {
                if (response.Success)
                {
                    Products = response.Data;
                }
            }
        }
    


    }
}
