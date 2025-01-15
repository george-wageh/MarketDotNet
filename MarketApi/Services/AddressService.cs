using MarketApi.IRepositories;
using MarketApi.UnitWork;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class AddressService
    {
        public AddressService(IAddressRepository  addressRepository , UnitWorkApp UnitWorkApp)
        {
            AddressRepository = addressRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        private IAddressRepository AddressRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }
        
        public async Task<ResponseDTO<AddressDTO>> GetAsync(string userId , int id)
        {
            var address = await AddressRepository.GetAsync(id);
            if (address != null)
            {
                if (address.UserId == userId)
                {
                    return new ResponseDTO<AddressDTO>
                    {
                        Data = new AddressDTO
                        {
                            Id = address.Id,
                            City = address.city,
                            Country = address.Country,
                            FullName = address.FullName,
                            IsDefault = address.IsDefault,
                            PostalCode = address.PostalCode,
                            StreetAddress = address.StreetAddress,
                            Phone = address.Phone
                        },
                        Success = true,
                        Message = ""
                    };
                }
                else {
                    return new ResponseDTO<AddressDTO>
                    {
                        Data = null,
                        Success = false,
                        Message = "access denied"
                    };
                }
               
            }
            else {
                return new ResponseDTO<AddressDTO>
                {
                    Data = null,
                    Success = false,
                    Message = "Address not found"
                };
            }
 
        }

        public async Task<ResponseDTO<IEnumerable<AddressDTO>>> GetAllAsync(string userId) {

            var addresses = (await AddressRepository.GetAllAsync(userId)).Select(x => new AddressDTO
            {
                Id = x.Id,
                City = x.city,
                Country = x.Country,
                FullName = x.FullName,
                IsDefault = x.IsDefault,
                PostalCode = x.PostalCode,
                StreetAddress = x.StreetAddress,
                Phone = x.Phone

            }).ToList();

            return new ResponseDTO<IEnumerable<AddressDTO>>
            {
                Data = addresses,
                Success = true,
                Message = ""
            };
        }

        public async Task<ResponseDTO<AddressDTO>> AddAsync(string userId,AddressDTO addressDTO)
        {
            bool IsDefaultLocal = false;
            if (await AddressRepository.IsFirstAddressAsync(userId)) {
                IsDefaultLocal = true;
            }
            var address = new Models.Address
            {
                city = addressDTO.City,
                Country = addressDTO.Country,
                FullName = addressDTO.FullName,
                StreetAddress = addressDTO.StreetAddress,
                PostalCode = addressDTO.PostalCode,
                UserId = userId,
                IsDefault = IsDefaultLocal,
                Phone= addressDTO.Phone
            };
            await AddressRepository.AddAsync(address);
            await UnitWorkApp.SaveChangesAsync();
            return await this.GetAsync(userId , address.Id);
        }

        public async Task<ResponseDTO<AddressDTO>> EditAsync(string userId ,AddressDTO addressDTO)
        {
            var addressOP = await this.GetAsync(userId, addressDTO.Id);
            if (addressOP.Success)
            {
                var address = await AddressRepository.GetAsync(addressDTO.Id);
                {
                    address.StreetAddress = addressDTO.StreetAddress;
                    address.city = addressDTO.City;
                    address.FullName = addressDTO.FullName;
                    address.Country = addressDTO.Country;
                    address.PostalCode = addressDTO.PostalCode;
                    address.Phone = addressDTO.Phone;

                }
                AddressRepository.Edit(address);
                await UnitWorkApp.SaveChangesAsync();

                return await this.GetAsync(userId , address.Id);
            }
            else {
                return addressOP;
            }
        }
        public async Task<ResponseDTO<AddressDTO>> GetDefaultAsync(string userId) { 
         var address = await AddressRepository.GetDefaultAsync(userId);
            if (address != null) { 
                return new ResponseDTO<AddressDTO> { 
                    Data = new AddressDTO { 
                        StreetAddress = address.StreetAddress,
                        City = address.city,
                        FullName = address.FullName,
                        Country = address.Country,
                        IsDefault = address.IsDefault,
                        PostalCode = address.PostalCode,
                        Id = address.Id,
                        Phone = address.Phone,
                    },
                    Success = true,
                    Message =""
                };
            }
            return new ResponseDTO<AddressDTO>
            {
                Data = null,
                Success = false,
                Message = "The Default address not found"
            };

        }

        public async Task<ResponseDTO<object>> SetDefaultAsync(string userId, int  addressId)
        {
            var addressOP = await this.GetAsync(userId, addressId);
            if (addressOP.Success)
            {
                var oldAddressDefult = await AddressRepository.GetDefaultAsync(userId);
                if (oldAddressDefult != null) { 
                    oldAddressDefult.IsDefault = false;
                }

                var address = await AddressRepository.GetAsync(addressId);
                address.IsDefault = true;

                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Success = true,
                    Data = "",
                    Message = ""

                };
            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Data = "",
                    Message = addressOP.Message

                };
            }
        }
        public async Task<ResponseDTO<object>> DeleteAsync(string userId, int id)
        {
            var addressOP = await this.GetAsync(userId,id);
            if (addressOP.Success)
            {
                var address = await AddressRepository.GetAsync(id);
                if (address.IsDefault == true) {
                    return new ResponseDTO<object>
                    {
                        Success = false,
                        Data = "",
                        Message = "Set the default address before deleting this address."
                    };
                }
                AddressRepository.Delete(address);
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Success = true,
                    Data = "",
                    Message = ""

                };
            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Data = null,
                    Message = addressOP.Message

                };
            }
        }


    }
}
