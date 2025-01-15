using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Category
{

    
    public partial class Categories
    {
        [Inject]
        public CategoriesService CategoriesService { get; set; }

        CategoryNodeDTO categoryNodeRoot = null;

        public CategoryDTO CategoryEditable { get; set; } = null;
        Dictionary<int, CategoryNodeDTO> categoryDTOsDic = null;
    protected override async Task OnInitializedAsync()
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
                }
            }
            StateHasChanged();
        }

        public void OpenEdit(CategoryNodeDTO categoryNodeDTO) {
            CategoryEditable = new CategoryDTO { 
                Name = categoryNodeDTO.Name,
                ParentId = categoryNodeDTO.ParentId,
                Id= categoryNodeDTO.Id
            };
            StateHasChanged();
        }

        public async Task OnSaveCategory(CategoryDTO categoryDTO) {
            if (categoryDTO == null)
            {
                CategoryEditable = null;
            }
            else {
               var responseDTO =await CategoriesService.EditCategory(categoryDTO.Id , categoryDTO.Name);
                if (responseDTO != null) {
                    if (responseDTO.Success) {
                        categoryDTOsDic[categoryDTO.Id].Name = categoryDTO.Name;
                        StateHasChanged();
                        CategoryEditable = null;

                    }
                }
            }
        }


    }
}
