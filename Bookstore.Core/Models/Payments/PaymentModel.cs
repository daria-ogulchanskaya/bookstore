using Bookstore.Core.Models.Orders;

namespace Bookstore.Core.Models.Payments
{
    public class PaymentModel : Model
    {
        public OrderModel Order { get; set; }

        public string Token { get; set; }
    }
}
