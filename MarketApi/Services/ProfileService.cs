using MarketApi.Models;
using MarketApi.UnitWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class ProfileService
    {
        public ProfileService(UserManager<ApplicationUser> userManager, UnitWorkApp UnitWorkApp)
        {
            UserManager = userManager;
            this.UnitWorkApp = UnitWorkApp;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public UnitWorkApp UnitWorkApp { get; }

        public async Task<ResponseDTO<ProfileDTO>> GetAsync(string userId) {
            var user = await UserManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                return new ResponseDTO<ProfileDTO>
                {
                    Message = "",
                    Success = true,
                    Data = new ProfileDTO
                    {
                        Email = user.Email,
                        FullName = user.FullName,
                        Phone = user.PhoneNumber,
                        UserName = user.UserName
                    }
                };
            }
            else {
                return new ResponseDTO<ProfileDTO>
                {
                    Message = "User not found",
                    Success = false,
                    Data = null
                };
            }
        }
    }
}
