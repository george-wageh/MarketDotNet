using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Product
{
    public partial class Comments
    {
        [Inject]
        public CommentsService commentsService { get; set; }
        [Parameter]
        public int productId { get; set; }

        public IEnumerable<CommentDTO> Comments_ { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var response = await commentsService.GetCommentsProduct(productId);
            if (response != null)
            {
                if (response.Success)
                {
                    Comments_ = response.Data;
                }
            }
        }
    }
}
