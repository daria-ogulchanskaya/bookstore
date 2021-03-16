using System;
using System.Collections.Generic;

namespace Bookstore.Core.Models.Orders
{
    public class AddOrderModel
    {
        public Guid UserId { get; set; }

        public List<OrderItemModel> Items;
    }
}
