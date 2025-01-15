using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users.Orders
{
    public partial class Order
    {
        [Parameter]
        public string UserName { get; set; }

        [Parameter]
        public int? Id { get; set; } = null;

        [Inject]      
        public OrdersService OrdersService { get; set; }

        public OrderDTO OrderDTO { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            //return base.OnParametersSetAsync();
            if (Id != null)
            {
                var response = await OrdersService.GetOrderAsync(UserName , Id);
                if (response != null)
                {
                    if (response.Success)
                    {
                        OrderDTO = response.Data;
                    }
                }
            }
        }
    }
}
