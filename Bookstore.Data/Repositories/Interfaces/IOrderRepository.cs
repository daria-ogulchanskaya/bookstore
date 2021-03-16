using Bookstore.Data.Entities;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Bookstore.Data.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<IPagedList<Order>> GetPageAsync(PaginationRequest<OrderFilter> request);

        public Task<IEnumerable<Order>> GetUserOrdersAsync(Guid id);
    }
}
