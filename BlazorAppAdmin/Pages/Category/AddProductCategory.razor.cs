using Microsoft.AspNetCore.Components;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Category
{
    public partial class AddProductCategory
    {

        protected override void OnInitialized()
        {
            Item = new ProductDTO() { Id = 0 };
        }
        [Parameter]
        public CategoryDTO categoryDTO { get; set; }
        public override async Task SaveAsync()
        {
            if (categoryDTO != null && Item != null)
            {
                Item.CategoryId = categoryDTO.Id;
                await OnSaveChanges.InvokeAsync(Item);
                await HideModal();
                Item = new ProductDTO { Id = 0 };
                StateHasChanged();
            }

        }
    }
}
