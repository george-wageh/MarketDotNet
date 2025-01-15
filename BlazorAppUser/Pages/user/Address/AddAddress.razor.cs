
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Address
{
    public partial class AddAddress
    {
        protected override void OnInitialized()
        {
            Item = new AddressDTO() { Id = 0 , IsDefault =false};
        }

        public override async Task SaveAsync()
        {
            await OnSaveChanges.InvokeAsync(Item);
            HideModal();
            Item = new AddressDTO { Id = 0, IsDefault = false };
        }

    }
}
