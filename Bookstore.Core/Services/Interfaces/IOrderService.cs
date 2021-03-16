using Bookstore.Core.Models.Orders;
using Bookstore.Core.Models.Payments;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Bookstore.Core.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderModel> GetAsync(Guid id);

        public Task<PagedResponse<OrderModel>> GetPageAsync(PaginationRequest<OrderFilter> page);

        public Task<IEnumerable<OrderModel>> GetUserOrdersAsync(Guid id);

        public Task<IEnumerable<OrderModel>> AllAsync();

        public Task<OrderModel> AddAsync(List<OrderItemModel> items, Guid id);

        public Task RemoveAsync(Guid id);

        public Task ChargeAsync(PaymentModel payment, string email);

        public Task DeletePaymentAsync(Guid id);
    }
}
