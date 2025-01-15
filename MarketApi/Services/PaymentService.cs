using MarketApi.IRepositories;
using MarketApi.Migrations;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using SharedLib.DTO;
using System;

namespace MarketApi.Services
{
    public class PaymentService
    {
        public PaymentService(IPaymentRepository paymentRepository, UnitWorkApp UnitWorkApp)
        {
            PaymentRepository = paymentRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        public IPaymentRepository PaymentRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }

        public async Task<ResponseDTO<PaymentCardDTO>> AddPaymentCard(string userId, PaymentDTO paymentDTO)
        {
            bool IsDefaultLocal = false;
            if (await PaymentRepository.IsFirstPaymentCardAsync(userId))
            {
                IsDefaultLocal = true;
            }
            var paymant = new Payment
            {
                CardNumber = paymentDTO.CardNumber,
                CVVCode = paymentDTO.CVVCode,
                ExpDate = paymentDTO.ExpDate.ToDateTime(new TimeOnly()),
                Name = paymentDTO.Name,
                IsDefault = IsDefaultLocal,
                UserId = userId
            };
            await PaymentRepository.AddPaymentCard(paymant);
            await UnitWorkApp.SaveChangesAsync();
            return await this.GetAsync(userId, paymant.Id);
        }

        public async Task<ResponseDTO<PaymentCardDTO>> GetAsync(string userId, int id)
        {
            var payment = await PaymentRepository.GetAsync(userId, id);
            if (payment != null)
            {
                return new ResponseDTO<PaymentCardDTO>
                {
                    Data = new PaymentCardDTO
                    {
                        Id = payment.Id,
                        Name = payment.Name,
                        ExpDate = DateOnly.FromDateTime(payment.ExpDate),
                        IsDefault = payment.IsDefault,
                        Last4digits = payment.CardNumber.Substring(payment.CardNumber.Length - 4)
                    },
                    Success = true,
                    Message = ""
                };
            }
            else
            {
                return new ResponseDTO<PaymentCardDTO>
                {
                    Data = null,
                    Success = false,
                    Message = "Payment not found"
                };
            }
        }
        public async Task<ResponseDTO<IEnumerable<PaymentCardDTO>>> GetAllAsync(string userId)
        {
            var payments = await PaymentRepository.GetAllAsync(userId);
            var paymentsDTO = payments.Select(payment => new PaymentCardDTO
            {
                Id = payment.Id,
                Name = payment.Name,
                ExpDate = DateOnly.FromDateTime(payment.ExpDate),
                IsDefault = payment.IsDefault,
                Last4digits = payment.CardNumber.Substring(payment.CardNumber.Length - 4)

            });
            return new ResponseDTO<IEnumerable<PaymentCardDTO>>
            {
                Success = true,
                Data = paymentsDTO,
                Message = ""
            };

        }
        public async Task<ResponseDTO<PaymentCardDTO>> GetDefaultAsync(string userId)
        {
            var payment = await PaymentRepository.GetDefaultAsync(userId);
            if (payment != null)
            {
                return new ResponseDTO<PaymentCardDTO>
                {
                    Data = new PaymentCardDTO
                    {
                        Id = payment.Id,
                        Name = payment.Name,
                        ExpDate = DateOnly.FromDateTime(payment.ExpDate),
                        IsDefault = payment.IsDefault,
                        Last4digits = payment.CardNumber.Substring(payment.CardNumber.Length - 4)
                    },
                    Success = true,
                    Message = ""
                };
            }
            return new ResponseDTO<PaymentCardDTO>
            {
                Data = null,
                Success = false,
                Message = "The Default payment not found"
            };
        }


        public async Task<ResponseDTO<object>> SetDefaultAsync(string userId, int id)
        {
            var oldPaymentDefult = await PaymentRepository.GetDefaultAsync(userId);

            if (oldPaymentDefult!=null)
            {

                var newPayment = await PaymentRepository.GetAsync(userId , id);
                if (newPayment != null)
                {

                    oldPaymentDefult.IsDefault = false;
                    newPayment.IsDefault = true;

                    await UnitWorkApp.SaveChangesAsync();
                    return new ResponseDTO<object>
                    {
                        Success = true,
                        Data = "",
                        Message = ""

                    };

                }
                else {
                    return new ResponseDTO<object>
                    {
                        Success = false,
                        Data = "",
                        Message = "New product not found"
                    };
                }
              
            }
            else
            {
                return new ResponseDTO<object>
                {
                    Success = false,
                    Data = "",
                    Message = "Old product not found"
                };
            }
        }


        public async Task<ResponseDTO<object>> DeletePaymentCard(string userId, int id)
        {
            var payment = await PaymentRepository.GetAsync(userId, id);

            if (payment != null)
            {
                if (payment.IsDefault == true)
                {
                    return new ResponseDTO<object>
                    {
                        Success = false,
                        Data = "",
                        Message = "Set the default payment before deleting this payment."
                    };
                }
                PaymentRepository.DeletePaymentCard(payment);
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
                    Message = "payment not found"

                };
            }
        }
    }
}
