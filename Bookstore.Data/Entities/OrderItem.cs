using Dapper.Contrib.Extensions;
using System;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Data.Entities
{
    [Table("Items")]
    public class OrderItem : Entity
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public Guid PrintingEditionId { get; set; }

        public PrintingEdition PrintingEdition { get; set; }

        public int Count { get; set; }

        public CurrencyType Currency { get; set; }
    }
}
