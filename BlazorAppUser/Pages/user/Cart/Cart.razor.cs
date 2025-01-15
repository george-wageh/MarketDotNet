using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Cart
{
    public partial class Cart
    {
        [Inject]
        AddressService AddressService { get; set; }

        [Inject]
        PaymentsService PaymentsService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        OrdersService OrdersService { get; set; }
        public AddressDTO address { get; set; }
        public PaymentCardDTO payment { get; set; }

        private List<ResponseDTO<object>> Messages { get; } = new();

        [Inject]
        public CartService CartService { get; set; }
        public IEnumerable<ProductQuantityCardDTO> Products { get; set; } = null;

        public int NextPageCount { get; set; } = 1;
        public void nextPage()
        {
            NextPageCount += 1;
            Console.WriteLine(NextPageCount);
        }
        protected async override Task OnInitializedAsync()
        {
            {
                var response = await AddressService.GetDefaultAsync();
                if (response != null) {
                    if (response.Success)
                    {
                        address = response.Data;
                    }
                    else
                    {
                        var message = new ResponseDTO<object>
                        {
                            Message = response.Message,
                            Success = false
                        };
                        AddMessage(message);
                    }
                }
            }
            {
                var response = await PaymentsService.GetDefaultAsync();
                if (response != null) {
                    if (response.Success)
                    {
                        payment = response.Data;
                    }
                    else
                    {
                        var message = new ResponseDTO<object>
                        {
                            Message = response.Message,
                            Success = false
                        };
                        AddMessage(message);
                    }
                }
              
            }
            {
                var response = await CartService.GetAllProductsAsync();
                if (response != null)
                {
                    if (response.Success)
                    {
                        Products = response.Data;
                    }
                    else
                    {
                        var message = new ResponseDTO<object>
                        {
                            Message = response.Message,
                            Success = false
                        };
                        AddMessage(message);
                    }
                }
            }
            
        }
        private async Task AddMessage(ResponseDTO<object> newMessage)
        {
            Messages.Add(newMessage);
            StateHasChanged();
            await Task.Delay(3000);
            Messages.Remove(newMessage);
            StateHasChanged();
        }

        public void UpdateProducts(IEnumerable<ProductQuantityCardDTO> Products) {
            this.Products = Products;
            StateHasChanged();
        }

        public async Task PlaceOrder()
        {
            var response = await OrdersService.PlaceOrder();
            if (response != null) {
                if (response.Success) {
                    Products = new List<ProductQuantityCardDTO>();
                    StateHasChanged();
                    NavigationManager.NavigateTo("/orders");
                }
            }

        }
    }
}
