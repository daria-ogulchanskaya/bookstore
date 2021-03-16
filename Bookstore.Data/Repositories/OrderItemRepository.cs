using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.EF;
using Bookstore.Data.Repositories.Interfaces;

namespace Bookstore.Data.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext databaseContext)
            : base(databaseContext)
        { }
    }
}
