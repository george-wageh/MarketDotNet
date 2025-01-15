using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users.Orders
{
    public partial class Orders
    {
        [Inject]
        public OrdersService OrdersService { get; set; }
        [Parameter]
        public string UserName { get; set; }

        public IEnumerable<OrderCardDTO> OrdersList { get; set; } = null;
        protected async override Task OnInitializedAsync()
        {
            var response = await OrdersService.GetAllAsync(UserName);
            if (response != null)
            {
                if (response.Success)
                {
                    OrdersList = response.Data;

                }
            }
        }


    }
}
