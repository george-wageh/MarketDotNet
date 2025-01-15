using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users
{
    public partial class Payments
    {
        [Inject]
        public PaymentsService PaymentsService { get; set; }

        public IEnumerable<PaymentCardDTO> paymentCardDTOs { get; set; }
        [Parameter]
        public string userName { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var response = await PaymentsService.GetAllAsync(userName);
            if (response != null)
            {
                if (response.Success)
                {
                    paymentCardDTOs = response.Data;
                }
            }
        }
    }
}
