using BlazorAppAdmin.Services;
using BlazorAppAdmin.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedLib.DTO;
using System;

namespace BlazorAppAdmin.Pages.Product
{
    public partial class EditProduct
    {
        public ResponseDTO<object> Message { get; set; } = null;
        private IBrowserFile? SelectedFile;
        private string? ImageToView = "";

        public int? CategoryId { get; set; } = null;

        [Inject]
        public ProductService ProductService { get; set; }
        [Parameter]
        public int Id { get; set; }

        private EditContext? editContext;


        private ValidationMessageStore? messageStore;
        public ProductDTO Product { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            var response = await ProductService.GetProductAsync(Id);
            if (response != null)
            {
                if (response.Success)
                {
                    Product = response.Data;
                    ImageToView = Product.ImageUrl;

                    Uri uri = new Uri(Product.ImageUrl);
                    Product.ImageUrl = uri.AbsolutePath.TrimStart('/');

                    CategoryId = Product.CategoryId;
                    editContext = new(Product);
                    editContext.OnValidationRequested += HandleValidationRequested;
                    messageStore = new(editContext);
                    StateHasChanged();
                }
            }
        }




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
            if (SelectedFile!=null && !SelectedFile.ContentType.StartsWith("image/"))
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
            var response = await ProductService.EditAsync(content, Product);
            if (response != null)
            {
                var message = new ResponseDTO<object>
                {
                    Success = response.Success,
                    Message = response.Success ? "Product updated successfully" : response.Message,
                };
                ShowMessage(message);
            }
        }
        public async Task OnChangeCategory(int categoryId)
        {
            CategoryId = categoryId;
            Product.CategoryId = categoryId;
        }

        private void RemoveImage() {

            Product.ImageUrl = "sfroot/images/products/temp-image.webp";
            ImageToView = ServerConfig.ServerDomain + this.Product.ImageUrl;
            SelectedFile = null;

        }
    }
}
