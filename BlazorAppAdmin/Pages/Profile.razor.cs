using BlazorAppUser.Services;
using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages
{
    public partial class Profile
    {
        private ProfileDTO? profileDTO;

        [Inject]
        public ProfileService ProfileService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var response = await ProfileService.GetAsync();
            if (response != null)
            {
                if (response.Success)
                {
                    profileDTO = response.Data;
                }
            }
        }
        public async Task Logout() {
            await ProfileService.Logout();

        }
    }
}
