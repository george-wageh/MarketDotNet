using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Orders
{
    public partial class Orders
    {
        [Inject]
        public OrdersService OrdersService { get; set; }

        public IEnumerable<OrderCardDTO> OrdersList { get; set; } = null;
        protected async override Task OnInitializedAsync()
        {
            var response = await OrdersService.GetAllAsync();
            if (response != null) {
                if (response.Success) {
                    OrdersList = response.Data;

                }
            }
        }


    }
}
