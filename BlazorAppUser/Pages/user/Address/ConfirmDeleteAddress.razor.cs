﻿using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Address
{
    public partial class ConfirmDeleteAddress
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