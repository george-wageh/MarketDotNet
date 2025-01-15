using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Category
{
    public partial class AddCategory
    {
        [Parameter]
        public int ParendId { get; set; }
        public CategoryDTO Category { get; set; } = new CategoryDTO { Id = 0, Name = "" };

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public CategoriesService CategoriesService { get; set; }

        public ResponseDTO<object> message { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        public async Task SaveAsync()
        {
            if (Category.Name.Count() < 4)
            {
                message = new ResponseDTO<object>()
                {
                    Data = "",
                    Message = "Invalid category name",
                    Success = false
                };
                return;
            }

            if (ParendId != 0)
            {
                Category.ParentId = ParendId;
                var response = await CategoriesService.AddCategory(Category);
                if (response != null)
                {
                    if (response.Success)
                    {
                        NavigationManager.NavigateTo("categories");
                        message = new ResponseDTO<object>()
                        {
                            Data = "",
                            Message = "Category added successfully",
                            Success = true
                        };
                        return;
                    }
                    else
                    {
                        message = new ResponseDTO<object>()
                        {
                            Data = "",
                            Message = response.Message,
                            Success = false
                        };
                        return;
                    }
                }
                else
                {
                    message = new ResponseDTO<object>()
                    {
                        Data = "",
                        Message = "Internal error 400",
                        Success = false
                    };
                    return;
                }

            }

            message = new ResponseDTO<object>()
            {
                Data = "",
                Message = "Select category parent",
                Success = false
            };
            return;

        }
    }
}
