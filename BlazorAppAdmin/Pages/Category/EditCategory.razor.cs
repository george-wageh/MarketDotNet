namespace BlazorAppAdmin.Pages.Category
{
    public partial class EditCategory
    {
        protected async override Task OnParametersSetAsync()
        {
            if (this.Item != null)
            {
                ShowModal();
            }
            return;
        }
        public async override Task SaveAsync()
        {
            await OnSaveChanges.InvokeAsync(Item);
        }
    }
}
