using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Category
{
    public partial class CategoryProducts
    {
        public IEnumerable<ProductCardDTO> productCardDTOs { get; set; }

        [Parameter]
        public CategoryDTO CategoryDTO { get; set; }

        public IEnumerable<ProductCardDTO> products { get; set; }

        [Inject]
        ProductService ProductService { get; set; }
        protected async override Task OnParametersSetAsync()
        {
            //if (CategoryDTO != null)
            //{
            //    var response = await ProductService.GetAllInCategoryAsync(CategoryDTO.Id);
            //    if (response != null)
            //    {
            //        if (response.Success)
            //        {
            //            products = response.Data;
            //            StateHasChanged();
            //        }
            //    }

            //}
        }

        public async Task saveProduct(ProductDTO productDTO)
        {
            //var response = await ProductService.AddAsync(productDTO);
            //if (response != null)
            //{
            //    if (response.Success)
            //    {
            //        products = products.Append(response.Data);
            //        StateHasChanged();
            //    }
            //}
            return;
        }
    }

}
