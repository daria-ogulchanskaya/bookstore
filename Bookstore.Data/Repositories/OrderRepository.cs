using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using System.Collections.Generic;
using System;
using Bookstore.Shared.Models.Page;
using Bookstore.Shared.Models.Filter;
using Bookstore.Data.Repositories.EF;
using static Microsoft.EntityFrameworkCore.EF;

namespace Bookstore.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext databaseContext)
            : base(databaseContext)
        { 
        }

        public async Task<IPagedList<Order>> GetPageAsync(PaginationRequest<OrderFilter> request)
        {
            IQueryable<Order> orders = _set.Include(x => x.Items)
                                           .ThenInclude(x => x.PrintingEdition)
                                           .Include(x => x.User)
                                           .Include(x => x.Payment);

            if (!(request.Filter is null))
            {
                orders = orders.Where(x => x.Status == request.Filter.Status);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                orders = orders.Where(x => Functions.Like(x.Description.ToLower(), $"%{request.SearchText.ToLower()}%"));
            }

            var paged = await orders.ToPagedListAsync(request.PageNumber, request.PageSize);
            return paged;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid id)
        {
            return await _set.Where(x => x.UserId == id)
                             .Include(x => x.Items)
                             .ThenInclude(x => x.PrintingEdition)
                             .Include(x => x.User)
                             .Include(x => x.Payment)
                             .ToListAsync();
        }

        public override async Task<IEnumerable<Order>> AllAsync()
        {
            return await _set.Include(x => x.User)
                             .Include(x => x.Payment)
                             .Include(x => x.Items)
                             .ThenInclude(x => x.PrintingEdition)
                             .ToListAsync();
        }

        public override async Task<Order> GetAsync(Guid id)
        {
            return await _set.Include(x => x.Items)
                             .ThenInclude(x => x.PrintingEdition)
                             .Include(x => x.User)
                             .Include(x => x.Payment)
                             .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
