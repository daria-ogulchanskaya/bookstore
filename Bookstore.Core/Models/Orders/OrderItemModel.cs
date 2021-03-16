using Bookstore.Core.Models.PrintingEdition;
using System;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Core.Models.Orders
{
    public class OrderItemModel : Model
    { 
        public Guid PrintingEditionId { get; set; }

        public PrintingEditionModel PrintingEdition { get; set; }

        public Guid OrderId { get; set; }

        public OrderModel Order { get; set; }

        public int Count { get; set; }

        public CurrencyType Currency { get; set; }
    }
}
