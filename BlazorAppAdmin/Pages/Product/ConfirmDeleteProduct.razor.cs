using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Product
{
    public partial class ConfirmDeleteProduct
    {
        protected override void OnParametersSet()
        {
            if (this.Item != null)
            {
                ShowModal();
            }
            return;
        }
        public async override Task SaveAsync()
        {
            await OnSaveChanges.InvokeAsync(Item);
        }
       
    }
}
