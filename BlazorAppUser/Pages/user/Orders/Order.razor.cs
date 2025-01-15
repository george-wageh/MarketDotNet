using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Orders
{
    public partial class Order
    {
        [Parameter]
        public int? Id { get; set; } = null;

        [Inject]
        public OrdersService OrdersService { get; set; }

        public OrderDTO OrderDTO { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            //return base.OnParametersSetAsync();
            if (Id != null) { 
                var response = await  OrdersService.GetOrderAsync(Id);
                if (response != null) {
                    if (response.Success) { 
                        OrderDTO = response.Data;
                    }
                }
            }
        }
    }
}
