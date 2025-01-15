using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Cart
{
    public partial class SelectAddress
    {
        [Inject]
        public AddressService AddressService { get; set; }


        [Inject]
        public NavigationManager Navigation { get; set; }



        [Parameter]
        public AddressDTO address { get; set; }

        public void GoToChangeAddress() {
            Navigation.NavigateTo("/address");
        }
    }
}
