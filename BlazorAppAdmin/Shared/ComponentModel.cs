using Microsoft.AspNetCore.Components;
using SharedLib.DTO;
using System.ComponentModel;

namespace BlazorAppAdmin.Shared
{
    public abstract partial class ComponentModel<T> : ComponentBase where T : class
    {
        [Parameter]
        public T? Item { get; set; }


        [Parameter]
        public EventCallback<T?> OnSaveChanges { get; set; }

        public bool isModalVisible { get; set; }

        public void ShowModal()
        {
            isModalVisible = true;
            StateHasChanged();
        }

        public async Task HideModal()
        {
            isModalVisible = false;
            StateHasChanged();
            await OnSaveChanges.InvokeAsync(null);

        }


        public abstract Task SaveAsync();

    }
}
