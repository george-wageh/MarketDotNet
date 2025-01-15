using BlazorAppAdmin.Pages.Category;
using Microsoft.AspNetCore.Components;

namespace BlazorAppUser.Pages.user.Products
{
    public partial class CategoryNodeComponent
    {
        [Parameter]
        public CategoryNodeDTO categoryNode { set; get; }

        [Parameter]
        public EventCallback<int> eventCallback { set; get; }
    }
}
