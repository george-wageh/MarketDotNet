using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.account
{
    public partial class Register
    {
        ResponseDTO<object> message { get; set; }


        public UserRegisterDTO UserRegister { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AccountService AccountService { get; set; }

        protected override void OnInitialized()
        {
            this.UserRegister = new UserRegisterDTO
            {
                Email = "",
                FullName = "",
                Password = "",
                Phone = ""
            };
        }
        public async Task ValidSubmit()
        {
            var response = await AccountService.RegisterAsync(this.UserRegister);
            if (response != null)
            {
                if (response.Success)
                {
                    NavigationManager.NavigateTo("/login");
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
    }
}
