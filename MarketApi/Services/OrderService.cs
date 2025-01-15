using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class OrderService
    {
        public OrderService(IOrderRepository OrderRepository,
            ICartRepository CartRepository, IAddressRepository addressRepository,
            IPaymentRepository paymentRepository,
            UnitWorkApp UnitWorkApp)
        {
            this.OrderRepository = OrderRepository;
            this.CartRepository = CartRepository;
            AddressRepository = addressRepository;
            PaymentRepository = paymentRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        public IOrderRepository OrderRepository { get; }
        public ICartRepository CartRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }

        public async Task<ResponseDTO<object>> PlaceOrder(string userId)
        {
            var cartProducts = await this.CartRepository.GetCartProductsAsync(userId);
            if (cartProducts.Count() > 0)
            {
                var address = await AddressRepository.GetDefaultAsync(userId);
                var orderAddress = new OrderAddress
                {
                    city = address.city,
                    Country = address.Country,
                    FullName = address.FullName,
                    Phone = address.Phone,
                    PostalCode = address.PostalCode,
                    StreetAddress = address.StreetAddress
                };
                if (address == null) {
                    return new ResponseDTO<object>
                    {
                        Message = "Address not found",
                        Data = "",
                        Success = false
                    };
                }
                var payment = await this.PaymentRepository.GetDefaultAsync(userId);
                if (payment == null) {
                    return new ResponseDTO<object>
                    {
                        Message = "Payment not found",
                        Data = "",
                        Success = false
                    };
                }
                var orderPayment = new OrderPayment
                {
                    CardNumber = payment.CardNumber,
                    CVVCode = payment.CVVCode,
                    Name = payment.Name,
                    ExpDate = payment.ExpDate
                };
                var products = cartProducts.Select(x => new OrderProduct
                {
                    Product = x.Product,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price
                }).ToList();
                var total = products.Aggregate(0.0m, (s, x) => s + (x.UnitPrice * x.Quantity));
                var order = new Order
                {
                    UserId = userId,
                    OrderProducts = products,
                    CreationDate = DateTime.Now,
                    ShipPrice = 50,
                    OrderAddress = orderAddress,
                    OrderPayment = orderPayment,
                    Total = total
                };
                order = await OrderRepository.AddOrderAsync(order);
                this.CartRepository.DeleteAllCart(cartProducts);
                await UnitWorkApp.SaveChangesAsync();
                return new ResponseDTO<object>
                {
                    Message = "",
                    Data = "",
                    Success = true
                };
            }
            else {
                return new ResponseDTO<object>
                {
                    Message = "Cart is empty",
                    Data = "",
                    Success = false
                };
            }
         
        }

        public async Task<ResponseDTO<OrderDTO>> GetOrderAsync(string userId, int orderId)
        {
            var order = await OrderRepository.GetOrderAsync(userId, orderId);
            if (order != null)
            {
                return new ResponseDTO<OrderDTO>
                {
                    Success = true,
                    Data = new OrderDTO
                    {
                        CreationDate = order.CreationDate,
                        Id = order.Id,
                        Total = order.Total,
                        ShipPrice = order.ShipPrice,
                        Products = order.OrderProducts.Select(x => new ProductQuantityCardDTO
                        { Id = x.Product.Id, ImageUrl = x.Product.Image, Name = x.Product.Name, Price = x.UnitPrice , Quantity= x.Quantity }
                    ),
                        OrderStates = new List<OrderStateDTO>(),
                        Address = new AddressDTO {
                            FullName = order.OrderAddress.FullName,
                            City= order.OrderAddress.city,
                            Country= order.OrderAddress.Country,
                            Id = order.OrderAddress.Id,
                            PostalCode = order.OrderAddress.PostalCode,
                            StreetAddress = order.OrderAddress.StreetAddress,
                            Phone = order.OrderAddress.Phone,
                        },
                        Payment = new PaymentCardDTO {
                            Id = order.OrderPayment.Id,
                            Name = order.OrderPayment.Name,
                            ExpDate = DateOnly.FromDateTime(order.OrderPayment.ExpDate),
                            Last4digits = order.OrderPayment.CardNumber.Substring(order.OrderPayment.CardNumber.Length - 4)
                        }
                    },
                    Message = ""
                };
            }
            else
            {
                return new ResponseDTO<OrderDTO>
                {
                    Success = false,
                    Data =  null,
                    Message = ""
                };
            }
        }
    
        public async Task<ResponseDTO<IEnumerable<OrderCardDTO>>> GetAllAsync(string userId)
        {
            var orders = await OrderRepository.GetAllAsync(userId);
            if (orders != null)
            {
                return new ResponseDTO<IEnumerable<OrderCardDTO>>
                {
                    Success = true,
                    Data = orders.Select(x=>new OrderCardDTO { 
                        CreationDate = x.CreationDate,
                        Id=x.Id,
                        ShipPrice=x.ShipPrice,
                        Total = x.Total,
                        productImages = x.OrderProducts.Select(x=>x.Product).Select(x=>x.Image).ToList()
                    }).ToList(),
                    Message = ""
                };
            }
            else
            {
                return new ResponseDTO<IEnumerable<OrderCardDTO>>
                {
                    Success = true,
                    Data = null,
                    Message = ""
                };
            }
        }
    }
}
