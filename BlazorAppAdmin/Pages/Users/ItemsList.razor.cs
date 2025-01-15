using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users
{
    public partial class ItemsList
    {
        [Parameter]
        public IEnumerable<ProductCardDTO> products { get; set; }
    }
}
