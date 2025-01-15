using BlazorAppUser.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Address
{
    public partial class Address
    {
        [Inject]
        public AddressService AddressService { get; set; }

        public IEnumerable<AddressDTO>? addresses { get; set; }

        public AddressDTO? ModelToEdit { get; set; } = null;

        public AddressDTO? ModelToDelete { get; set; } = null;

        public ResponseDTO<object>? message { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            var response = await AddressService.GetAllAsync();
            if (response.Success)
            {
                addresses = response.Data;
            }
        }


        private async Task ShowMessageAsync(ResponseDTO<object> message_)
        {
            this.message = message_;
            StateHasChanged();
            await Task.Delay(3000);
            message = null;
            StateHasChanged();
        }


        private async Task HandleSaveNewAddress(AddressDTO address)
        {
            var response = await AddressService.AddAsync(address);
            if (response != null)
            {
                if (response.Success)
                {
                    if (addresses == null)
                    {
                        addresses = new List<AddressDTO>();
                    }
                    var message_ = new ResponseDTO<object>() { Data = "", Message = "Address Added Successfully", Success = true };
                    addresses = addresses.Append(response.Data);
                    ShowMessageAsync(message_);
                }
                else {
                    var message_ = new ResponseDTO<object>() { Data = "", Message = response.Message, Success = false };
                    ShowMessageAsync(message_);
                }
            }
        }


        private async Task HandleSaveEditAddress(AddressDTO address)
        {
            if (address != null)
            {
                var response = await AddressService.EditAsync(address);
                if (response != null)
                {
                    if (response.Success)
                    {
                        var oldAddress = addresses.Where(x => x.Id == address.Id).FirstOrDefault();
                        if (oldAddress != null) {
                            var newAddress = response.Data;
                            {
                                oldAddress.StreetAddress = newAddress.StreetAddress;
                                oldAddress.PostalCode = newAddress.PostalCode;
                                oldAddress.IsDefault = newAddress.IsDefault;
                                oldAddress.FullName = newAddress.FullName;
                                oldAddress.City = newAddress.City;
                                oldAddress.Phone = newAddress.Phone;
                            }
                        }
                        var message_ = new ResponseDTO<object>() { Data = "", Message = "Address Saved Successfully", Success = true };
                        ShowMessageAsync(message_);

                    }
                    else
                    {
                        var message_ = new ResponseDTO<object>() { Data = "", Message = response.Message, Success = false };
                        ShowMessageAsync(message_);
                    }
                }
            }
            ModelToEdit = null;
            StateHasChanged();
        }



        private async Task HandleSaveDeleteAddress(AddressDTO addressDTO)
        {

            if (addressDTO != null)
            {
                var response = await AddressService.DeleteAsync(addressDTO);
                if (response != null)
                {
                    if (response.Success)
                    {
                        addresses = addresses.Where(x => x.Id != addressDTO.Id).ToList();
                        var message_ = new ResponseDTO<object>() { Data = "", Message = "Address Deleted Successfully", Success = true };
                        ShowMessageAsync(message_);
                    }
                    else
                    {
                        var message_ = new ResponseDTO<object>() { Data = "", Message = response.Message, Success = false };
                        ShowMessageAsync(message_);
                    }
                }
            }
            ModelToDelete = null;
        }

        private void EditAddressAsync(int addressId)
        {
            if (addresses != null)
            {
                ModelToEdit = addresses.FirstOrDefault(x => x.Id == addressId)?.Clone();
                StateHasChanged();
            }

        }


        private void DeleteAddress(int addressId)
        {
            if (addresses != null)
            {
                ModelToDelete = addresses.FirstOrDefault(x => x.Id == addressId);
                StateHasChanged();
            }
        }


        private async Task setDefault(int addressId)
        {
            var response = await AddressService.setDefaultAsync(addressId);
            if (response != null)
            {
                if (response.Success)
                {
                    var OldAddresses = addresses.Where(x => x.IsDefault == true).ToList();
                    foreach (var item in OldAddresses)
                    {
                        item.IsDefault = false;
                    }

                    var newAddress = addresses.Where(x => x.Id == addressId).FirstOrDefault();
                    newAddress.IsDefault = true;


                    var message_ = new ResponseDTO<object>() { Data = "", Message = "Address saved as default successfully", Success = true };
                    ShowMessageAsync(message_);

                }
                else
                {
                    var message_ = new ResponseDTO<object>() { Data = "", Message = response.Message, Success = false };
                    ShowMessageAsync(message_);
                }
            }
        }
    }
}
