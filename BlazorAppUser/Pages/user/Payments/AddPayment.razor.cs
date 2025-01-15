using BlazorAppUser.Shared;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Payments
{
    public partial class AddPayment
    {
        protected override void OnInitialized()
        {
            //base.OnInitialized();
            Item = new PaymentDTO { Id = 0 };
        }
        public async override Task SaveAsync()
        {
            await this.OnSaveChanges.InvokeAsync(Item);
            HideModal();
            Item = new PaymentDTO { Id = 0 };
        }
    }
}
