using BlazorAppAdmin.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.account
{
    public partial class Login
    {
        ResponseDTO<object> message { get; set; }

        [Inject]
        AccountService AccountService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ILocalStorageService LocalStorage { get; set; }
        public UserLoginDTO UserLogin { get; set; }
        private async Task ValidSubmit() {
            var response = await AccountService.Login(UserLogin);
            if (response != null)
            {
                if (response.Success)
                {
                    string token = (string)response.Data;
                    await LocalStorage.SetItemAsync("token", token);
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    message = new ResponseDTO<object>()
                    {
                        Data = response.Data,
                        Success = false,
                        Message = response.Message
                    };
                }
            }
            else
            {
                message = new ResponseDTO<object>()
                {
                    Data = "",
                    Success = false,
                    Message = "Internal error 500"
                };
            }
        }

        protected override void OnInitialized()
        {
            UserLogin = new UserLoginDTO
            {
                Email = "",
                Password = ""
            };
        }
     
    }
}
