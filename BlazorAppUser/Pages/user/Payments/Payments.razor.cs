using BlazorAppUser.Pages.user.Address;
using BlazorAppUser.Pages.user.Payments;
using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Payments
{
    public partial class Payments
    {
        [Inject]
        public PaymentsService paymentsService { get; set; }

        public IEnumerable<PaymentCardDTO> paymentCardDTOs { get; set; }

        public ResponseDTO<object>? message { get; set; } = null;

        public PaymentCardDTO? ModelToDelete { get; set; } = null;

        protected async override Task OnInitializedAsync()
        {
            var response = await paymentsService.GetAllAsync();
            if (response != null) {
                if (response.Success) {
                    paymentCardDTOs = response.Data;
                }
            }
        }
        private async Task ShowMessageAsync(ResponseDTO<object> message_)
        {
            this.message = message_;
            StateHasChanged();
            await Task.Delay(3000);
            message = null;
            StateHasChanged();
        }
        public async Task HandleSaveNewPayment(PaymentDTO paymentCardDTO) {
            var response = await paymentsService.AddAsync(paymentCardDTO);
            if (response != null)
            {
                if (response.Success)
                {
                    paymentCardDTOs = paymentCardDTOs.Append(response.Data);
                    var message_ = new ResponseDTO<object>() { Data = "", Message = "Payment Added Successfully", Success = true };
                    ShowMessageAsync(message_);


                }
            }
        }
        private void DeletePayment(int paymentId)
        {
            if (paymentCardDTOs != null)
            {
                ModelToDelete = paymentCardDTOs.FirstOrDefault(x => x.Id == paymentId);
                StateHasChanged();
            }
        }

        public async Task HandleDeletePayment(PaymentCardDTO paymentCardDTO)
        {
            if (paymentCardDTO != null) {
                var response = await paymentsService.DeleteAsync(paymentCardDTO.Id);
                if (response != null)
                {
                    if (response.Success)
                    {
                        paymentCardDTOs = paymentCardDTOs.Where(x => x.Id != paymentCardDTO.Id);
                        var message_ = new ResponseDTO<object>() { Data = "", Message = "Payment Removed Successfully", Success = true };
                        ShowMessageAsync(message_);
                    }
                    else
                    {
                        var message_ = new ResponseDTO<object>() { Data = "", Message = response.Message, Success = false };
                        ShowMessageAsync(message_);
                    }
                }
            }
            ModelToDelete = null;
            StateHasChanged();
        }
        private async Task setDefault(int cardId)
        {
            var response = await paymentsService.setDefaultAsync(cardId);
            if (response != null)
            {
                if (response.Success)
                {
                    var OldPayments = paymentCardDTOs.Where(x => x.IsDefault == true).ToList();
                    foreach (var item in OldPayments)
                    {
                        item.IsDefault = false;
                    }

                    var newPayment = paymentCardDTOs.Where(x => x.Id == cardId).FirstOrDefault();
                    newPayment.IsDefault = true;


                    var message_ = new ResponseDTO<object>() { Data = "", Message = "Payment saved as default successfully", Success = true };
                    ShowMessageAsync(message_);

                }
                else
                {
                    var message_ = new ResponseDTO<object>() { Data = "", Message = response.Message, Success = false };
                    ShowMessageAsync(message_);
                }
            }
        }

    }
}
