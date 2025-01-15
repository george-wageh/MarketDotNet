using MarketApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedLib.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketApi.Services
{
    public class AccountAdminService
    {
        public AccountAdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , IConfiguration  configuration)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IConfiguration Configuration { get; }

        public async Task<ResponseDTO<object>> Register(UserRegisterDTO userRegisterDTO)
        {
            var foundUser = await UserManager.FindByEmailAsync(userRegisterDTO.Email);
            if (foundUser != null)
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Message = "this account has already created please sign in ."
                };

            }
            var user = new ApplicationUser
            {
                Email = userRegisterDTO.Email,
                PhoneNumber = userRegisterDTO.Phone,
                FullName = userRegisterDTO.FullName,
                UserName = Guid.NewGuid().ToString(),
            };
            var result = await UserManager.CreateAsync(user, userRegisterDTO.Password);
            await UserManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return new ResponseDTO<object>
                {
                    Success = true,
                    Message = "Registration has been completed successfully."
                };

            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(x => x.Description)),
                    Data = result.Errors
                };
            }
        }
        public async Task<ResponseDTO<string>> Login(UserLoginDTO UserLoginDTO)
        {
            var foundUser = await UserManager.FindByEmailAsync(UserLoginDTO.Email);

            if (foundUser != null)
            {
                var HasAdmin = await UserManager.IsInRoleAsync(foundUser, "Admin");
                if (!HasAdmin)
                {
                    return new ResponseDTO<string>
                    {
                        Success = false,
                        Message = "Not have access",
                        Data = ""
                    };
                }
                var passwordCheck = await UserManager.CheckPasswordAsync(foundUser, UserLoginDTO.Password);
                if (passwordCheck == true)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, foundUser.UserName),
                            new Claim(JwtRegisteredClaimNames.Email, foundUser.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.Role,"Admin"),

                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Generate token
                    var token = new JwtSecurityToken(
                        issuer: Configuration["Jwt:Issuer"],
                        audience: Configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMonths(1),
                        signingCredentials: credentials
                    );

                    return new ResponseDTO<string>
                    {
                        Success = true,
                        Message = "",
                        Data = new JwtSecurityTokenHandler().WriteToken(token).ToString()
                    };
                }
                else {
                    return new ResponseDTO<string>
                    {
                        Success = false,
                        Message = "Password is incorrect"
                    };
                }
            }

            return new ResponseDTO<string>
            {
                Success = false,
                Message = "This account not found"
            };
        }
    }
}
