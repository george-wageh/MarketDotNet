using BlazorAppAdmin.Pages.Category;
using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppAdmin.Pages
{
    public partial class Categories
    {
        bool accordionShow { get; set; } = true;

        [Inject]
        public CategoriesService CategoriesService { get; set; }

        [Parameter]
        public int? CategoryId { get; set; }

        [Parameter]
        public EventCallback<int> OnChangeCategory { get; set; }

        Dictionary<int, CategoryNodeDTO> categoryDTOsDic = null;
        CategoryNodeDTO categoryNodeRoot = null;
        protected async override Task OnInitializedAsync()
        {
            //return base.OnInitializedAsync();
            var response = await CategoriesService.GetAllCategories();
            if (response != null)
            {
                if (response.Success)
                {
                    categoryDTOsDic = new Dictionary<int, CategoryNodeDTO>();
                    var categoryDTOs = response.Data.ToList();
                    foreach (var item in categoryDTOs)
                    {
                        var categoryNodeDTO = new CategoryNodeDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            ParentId = item.ParentId,
                        };
                        if (item.ParentId == null)
                        {
                            categoryNodeRoot = categoryNodeDTO;
                        }
                        if (!categoryDTOsDic.ContainsKey(item.Id))
                        {
                            categoryDTOsDic.Add(item.Id, categoryNodeDTO);
                        }

                    }
                    foreach (var item in categoryDTOs)
                    {
                        if (item.ParentId != null)
                        {
                            categoryDTOsDic[(int)item.ParentId].children.Add(categoryDTOsDic[item.Id]);
                        }

                    }
                    if (CategoryId != null) {
                        if (categoryDTOsDic.ContainsKey((int)CategoryId)) {
                            categoryNodeRoot = categoryDTOsDic[(int)CategoryId];
                        }
                    }
                    await OnChangeCategory.InvokeAsync(categoryNodeRoot.Id);

                }
            }

        }

        public async Task ToggleAccordion()
        {
            accordionShow = !accordionShow;
            StateHasChanged();
        }

        public async Task onSelectCategory(int categoryId)
        {

            if (categoryNodeRoot.Id != categoryId && categoryDTOsDic.ContainsKey(categoryId))
            {
                categoryNodeRoot = categoryDTOsDic[categoryId];

                await OnChangeCategory.InvokeAsync(categoryId);
                StateHasChanged();
            }


        }
        public async Task BackCategory()
        {
            if (categoryNodeRoot.ParentId != null)
            {
                categoryNodeRoot = categoryDTOsDic[(int)categoryNodeRoot.ParentId];
                await OnChangeCategory.InvokeAsync(categoryNodeRoot.Id);
                StateHasChanged();

            }


        }
    }
}
