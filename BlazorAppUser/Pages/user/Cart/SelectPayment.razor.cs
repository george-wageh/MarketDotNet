using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Cart
{
    public partial class SelectPayment
    {
        [Inject]
        public PaymentsService PaymentsService { get; set; }


        [Inject]
        public NavigationManager Navigation { get; set; }



        [Parameter]
        public PaymentCardDTO payment { get; set; }

        public void GoToChangePayment()
        {
            Navigation.NavigateTo("/payments");
        }
    }
}
