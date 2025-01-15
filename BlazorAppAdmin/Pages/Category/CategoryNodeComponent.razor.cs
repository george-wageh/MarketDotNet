using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Category
{
    public partial class CategoryNodeComponent
    {
        [Parameter]
        public CategoryNodeDTO categoryNode { set; get; }
        [Parameter]
        public EventCallback<CategoryNodeDTO> OpenEdit { set; get; }
        public bool AccordionShow { get; set; } = false;
        public void ToggleAccordion()=>AccordionShow = !AccordionShow;

        public string newName { get; set; } = "";

   
    }
}
