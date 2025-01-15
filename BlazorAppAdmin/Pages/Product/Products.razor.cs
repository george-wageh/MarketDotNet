using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Product
{
    public partial class Products
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public int? CategoryId { get; set; }

        [Inject] public ProductService ProductService { get; set; }


        public IEnumerable<ProductCardDTO> ProductsList { get; private set; }
        private List<ResponseDTO<object>> Messages { get; } = new();

        public string? Error { get; private set; }
        public bool AccordionShow { get; private set; } = true;

        public int Count { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;
        public int AllCount { get; private set; }
        public string? SortBy { get; set; } = "DefaultSort";
        public string? Qstring { get; set; }

        public void ToggleAccordion() => AccordionShow = !AccordionShow;

        //protected override async Task OnInitializedAsync() => await LoadProducts();
        ProductCardDTO productToDelete { get; set; } = null; 
        private ProductQueryDTO BuildQuery() =>
            new()
            {
                Count = Count,
                PageNum = CurrentPage,
                Qstring = Qstring,
                CategoryId = CategoryId,
                SortBy = SortBy
            };

        private async Task LoadProducts()
        {
            var response = await ProductService.GetProductsInQuery(BuildQuery());
            if (response != null) {
                if (response.Success)
                {
                    ProductsList = response.Data;
                    AllCount = response.Count;
                    Error = null;
                }
                else
                {
                    Error = response.Message;
                }
            }
         
        }

        private async Task AddMessage(string message)
        {
            var newMessage = new ResponseDTO<object> { Message = message, Success = true };
            Messages.Add(newMessage);
            await Task.Delay(3000);
            Messages.Remove(newMessage);
        }


        public async Task OnChangeCategory(int categoryId)
        {
            CategoryId = categoryId;
            CurrentPage = 1; // Reset to the first page
            await LoadProducts();
        }

        public async Task ChangeSort(string sort)
        {
            SortBy = sort;
            CurrentPage = 1; // Reset to the first page
            await LoadProducts();
        }

        public async Task SelectCount(int count)
        {
            Count = count;
            CurrentPage = 1; // Reset to the first page
            await LoadProducts();
        }

        public async Task ChangePage(int page)
        {
            if (page > 0 && page <= (int)Math.Ceiling((double)AllCount / Count))
            {
                CurrentPage = page;
                await LoadProducts();
            }
        }

        public async Task PreviousPage() => await ChangePage(CurrentPage - 1);

        public async Task NextPage() => await ChangePage(CurrentPage + 1);

        public async Task Search()
        {
            CurrentPage = 1; // Reset to the first page
            await LoadProducts();
        }

        public void OpenDeleteAlart(int productId) {
            productToDelete = ProductsList.FirstOrDefault(x => x.Id == productId);
        }

        public async Task ConfirmDelete(ProductCardDTO productCardDTO)
        {
            if (productCardDTO == null) { 
                productToDelete = null;
                return;
            }
            var response = await ProductService.DeleteAsync(productCardDTO.Id);
            if (response != null && response.Success) { 
                ProductsList = ProductsList.Where(x => x.Id != productCardDTO.Id).ToList();
                productToDelete = null;
            }
        }
    }
}
