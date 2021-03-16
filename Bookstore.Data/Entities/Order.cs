using System;
using System.Collections.Generic;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Data.Entities
{
    public class Order : Entity
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid PaymentId { get; set; }

        public Payment Payment { get; set; }

        public OrderStatusType Status { get; set; }

        public string Description { get; set; }

        public double TotalCost { get; set; }

        public List<OrderItem> Items { get; set; }

        public Order() => 
            Items = new List<OrderItem>();
    }
}
