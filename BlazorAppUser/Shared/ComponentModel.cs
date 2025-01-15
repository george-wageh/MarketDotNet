using Microsoft.AspNetCore.Components;
using SharedLib.DTO;
using System.ComponentModel;

namespace BlazorAppUser.Shared
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

        public void HideModal()
        {
            isModalVisible = false;
            StateHasChanged();
        }


        public abstract Task SaveAsync();

    }
}
