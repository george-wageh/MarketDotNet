using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.AdminDTO;

namespace BlazorAppAdmin.Pages.Category
{
    public partial class CategoryDetails
    {
        [Parameter]
        public int id { get; set; }

        [Inject]
        CategoriesService CategoriesService { get; set; }

        CategoryDTO categoryDTO { get; set; }
        protected async override Task OnParametersSetAsync()
        {
            var response = await CategoriesService.GetCategory(id);
            if (response != null)
            {
                if (response.Success)
                {
                    categoryDTO = response.Data;
                    StateHasChanged();
                }
            }
        }

    }



}
