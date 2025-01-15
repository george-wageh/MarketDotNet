using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class UserService
    {
        public UserService(IUserRepository userRepository )
        {
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public async Task<ResponseListDTO<IEnumerable<UserDTO>>> GetUsers(UserQueryDTO userQueryDTO) {
 
            var UserQuery = UserRepository.GetEmptyQueryable();

            if (userQueryDTO.Qstring != null && userQueryDTO.SearchBy != null && userQueryDTO.Qstring != "" && userQueryDTO.SearchBy != "")
            {
                if (userQueryDTO.SearchBy == "Phone")
                {
                    UserQuery = UserQuery.Union(UserRepository.SearchByPhone(userQueryDTO.Qstring));
                }
                else if (userQueryDTO.SearchBy == "Email")
                {
                    UserQuery = UserQuery.Union(UserRepository.SearchByEmail(userQueryDTO.Qstring));
                }
                else if (userQueryDTO.SearchBy == "Username")
                {
                    UserQuery = UserQuery.Union(UserRepository.SearchByUsername(userQueryDTO.Qstring));

                }
            }
            else {
                UserQuery = UserQuery.Union(UserRepository.GetAllUsers());
            }


             // Apply pagination
            if (userQueryDTO.Count == null)
            {
                userQueryDTO.Count = 5;  // Default to 5 if no Count specified
            }
            if (userQueryDTO.PageNum == null)
            {
                userQueryDTO.PageNum = 1;  // Default to first page if no PageNum specified
            }
            var Count = await UserQuery.CountAsync();

            var skip = (userQueryDTO.PageNum.Value - 1) * userQueryDTO.Count.Value;

            UserQuery = UserQuery.Skip(skip).Take(userQueryDTO.Count.Value);

            var Users = await UserQuery.Include(x=>x.Orders).Include(x=>x.CartProducts).Include(x=>x.FavoriteProducts).ToListAsync();
            //return;
      
            return new ResponseListDTO<IEnumerable<UserDTO>>
            {
                Count = Count,
                Data = Users.Select(x => new UserDTO
                {
                    Email = x.Email,
                    FullName = x.FullName,
                    Phone = x.PhoneNumber,
                    OrdersCount = x.Orders.Count(),
                    ProductsCountCart = x.CartProducts.Count(),
                    ProductsCountFav = x.FavoriteProducts.Count(),
                    UserName = x.UserName
                })
                ,
                Message="",
                Success=true

            };
        }
    }
}
