using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user
{
    public partial class FavoriteProducts
    {
        public IEnumerable<ProductCardDTO> Products { get; set; }

        private List<ResponseDTO<object>> Messages { get; } = new();

        [Inject]
        public FavoriteProductService service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //return base.OnInitializedAsync();
            var response = await service.GetAllFavListAsync();
            if (response != null)
            {
                if (response.Success)
                {
                    Products = response.Data;
                }
            }
        }
        private async Task AddMessage(string message)
        {
            var newMessage = new ResponseDTO<object> { Message = message, Success = true };
            Messages.Add(newMessage);
            await Task.Delay(3000);
            Messages.Remove(newMessage);
        }
        public async Task OnAddedToCart(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                await AddMessage($"Product ({product.Name}) added to cart");
            }
        }
        public async Task OnRemovedFromFav(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                if (product.IsFav) {
                    Products = Products.Where(x => x.Id != productId);
                    StateHasChanged();
                    await AddMessage($"Product ({product.Name}) removed from favorites list");
                }
            }
        }


    }
}
