using Microsoft.AspNetCore.Components;
using SharedLib.DTO;

namespace BlazorAppUser.Pages.user.Cart
{
    public partial class Summary
    {
        [Parameter]
        public IEnumerable<ProductQuantityCardDTO> Products { get; set; } = null;


        [Parameter]
        public AddressDTO address { get; set; }

        [Parameter]
        public PaymentCardDTO payment { get; set; }

        public double PTotal { get; set; }

        public double ShipPrice { get; set; }
        protected override void OnParametersSet()
        {
            PTotal = this.Products.Aggregate(0.0, (s, x) => s + (double)(x.Price * x.Quantity));
            ShipPrice = 50;
        }

    }
}
