using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Product
{
    public partial class ItemsList
    {
        [Parameter]
        public IEnumerable<ProductCardDTO> products { get; set; }
        [Parameter]
        public EventCallback<int> OnProductDelete { get; set; }
    }
}
