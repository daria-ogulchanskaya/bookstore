using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Shared.Models.Filter
{
    public class OrderFilter
    {
        public OrderStatusType Status { get; set; }
    }
}
