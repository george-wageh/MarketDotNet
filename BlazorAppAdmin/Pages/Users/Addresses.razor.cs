using BlazorAppAdmin.Pages.Product;
using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users
{
    public partial class Addresses
    {
        [Inject]
        public AddressService AddressService { get; set; }

        public IEnumerable<AddressDTO>? addresses { get; set; }

        [Parameter]
        public string UserName { get; set; }
        protected async override Task OnInitializedAsync()
        {
            var response = await AddressService.GetAllAsync(UserName);
            if (response != null)
            {
                if (response.Success)
                {
                    addresses = response.Data;
                }
            }

        }
    }
}
