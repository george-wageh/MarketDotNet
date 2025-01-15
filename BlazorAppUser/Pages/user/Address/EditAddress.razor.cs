using Microsoft.AspNetCore.Components;
using SharedLib.DTO;
using System.ComponentModel;

namespace BlazorAppUser.Pages.user.Address
{
    public partial class EditAddress
    {
        protected async override Task OnParametersSetAsync()
        {
            if (this.Item != null) {
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
