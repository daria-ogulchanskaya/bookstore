using Bookstore.Core.Models.Payments;
using Bookstore.Core.Models.Users;
using System;
using System.Collections.Generic;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Core.Models.Orders
{
    public class OrderModel : Model
    {
        public Guid UserId { get; set; }

        public UserModel User { get; set; }

        public Guid PaymentId { get; set; }

        public PaymentModel Payment { get; set; }

        public string Description { get; set; }

        public OrderStatusType Status { get; set; }

        public double TotalCost { get; set; }

        public List<OrderItemModel> Items;

        public OrderModel() => 
            Items = new List<OrderItemModel>();
    }
}
