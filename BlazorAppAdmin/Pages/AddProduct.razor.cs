using BlazorAppAdmin.Services;
using BlazorAppAdmin.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedLib.DTO;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace BlazorAppAdmin.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public ProductService ProductService { get; set; }


        private EditContext? editContext;


        private ValidationMessageStore? messageStore;
        public ProductDTO Product { get; set; } = new ProductDTO()
        {
            CategoryId = 4,
            Description = "",
            Id = 1,
            ImageUrl = "",
            IsFav = true,
            Name = "",
            Price = 99999
        };
        protected override void OnInitialized()
        {
            Product = new ProductDTO()
            {
                CategoryId = 4,
                Description = "",
                Id = 1,
                ImageUrl = "sfroot/images/products/temp-image.webp",
                IsFav = true,
                Name = "",
                Price = 99999
            };
            editContext = new(Product);
            editContext.OnValidationRequested += HandleValidationRequested;
            messageStore = new(editContext);
            ImageToView = ServerConfig.ServerDomain + this.Product.ImageUrl;

        }

        public ResponseDTO<object> Message { get; set; } = null;
        private IBrowserFile? SelectedFile;
        private string? ImageToView = "";

        [Parameter]
        [SupplyParameterFromQuery]
        public int? CategoryId { get; set; } = null;

        private async Task OnFileSelected(InputFileChangeEventArgs e)
        {
            SelectedFile = e.File;
            var buffer = new byte[SelectedFile.Size];
            await SelectedFile.OpenReadStream().ReadAsync(buffer);
            ImageToView = $"data:{SelectedFile.ContentType};base64,{Convert.ToBase64String(buffer)}";
        }
        private async Task ShowMessage(ResponseDTO<object> message)
        {
            Message = message;
            StateHasChanged();
            await Task.Delay(2000);
            Message = null;
            StateHasChanged();
        }

        private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
        {
            messageStore?.Clear();

            if (ImageToView == null || ImageToView == "")
            {
                messageStore?.Add(() => SelectedFile, "Select image please");
                return;
            }
            if (SelectedFile != null && !SelectedFile.ContentType.StartsWith("image/"))
            {
                messageStore?.Add(() => SelectedFile, "This file not image");
                return;
            }
        }
        private async Task HandleValidSubmit()
        {
            MultipartFormDataContent? content = null;
            if (SelectedFile != null)
            {
                content = new MultipartFormDataContent();
                var fileContent = new StreamContent(SelectedFile.OpenReadStream(maxAllowedSize: 10_000_000)); // 10MB limit
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(SelectedFile.ContentType);
                content.Add(fileContent, "file", SelectedFile.Name);
            }
            var response = await ProductService.AddAsync(content, Product);
            if (response != null)
            {
                var message = new ResponseDTO<object>
                {
                    Success = response.Success,
                    Message = response.Success ? "Product added successfully" : response.Message,
                };
                ShowMessage(message);
            }
        }
        public void Crear()
        {
            Product = new ProductDTO()
            {
                CategoryId = (int)this.CategoryId,
                Description = "",
                Id = 1,
                ImageUrl = "",
                IsFav = true,
                Name = "",
                Price = 99999
            };
            ImageToView = ServerConfig.ServerDomain + "sfroot/images/products/temp-image.webp";
            SelectedFile = null;
            StateHasChanged();
        }
        public async Task OnChangeCategory(int categoryId)
        {
            CategoryId = categoryId;
            Product.CategoryId = categoryId;
        }
    }
}
