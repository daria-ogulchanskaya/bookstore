using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Interfaces;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.EF;
using Bookstore.Data.Repositories.EF;
using System.Collections.Generic;

namespace Bookstore.Data.Repositories
{
    public class PrintingEditionRepository
        : Repository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext databaseContext)
            : base(databaseContext)
        { }

        public override async Task<IEnumerable<PrintingEdition>> AllAsync()
        {
            return await _set.Include(x => x.Authors)
                             .ThenInclude(x => x.Author)
                             .ToListAsync();
        }

        public override async Task<PrintingEdition> GetAsync(Guid id)
        {
            return await _set.Include(x => x.Authors)
                             .ThenInclude(x => x.Author)
                             .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IPagedList<PrintingEdition>> GetPageAsync(PaginationRequest<PrintingEditionFilter> page)
        {
            IQueryable<PrintingEdition> printingEditions = _set.Include(x => x.Authors)
                                                               .ThenInclude(x => x.Author);

            IPagedList<PrintingEdition> paged;

            if (!string.IsNullOrWhiteSpace(page.SearchText))
            {
                string searchText = page.SearchText.ToLower();

                printingEditions = printingEditions.Where(x => Functions.Like(x.Title.ToLower(), $"%{searchText}%") ||
                                                               Functions.Like(x.Description.ToLower(), $"%{searchText}%"));
            }
            if (page.Filter is null)
            {
                paged = await printingEditions.ToPagedListAsync(page.PageNumber, page.PageSize);
                return paged;
            }

            printingEditions = printingEditions.Where(x => page.Filter.Min != null && x.Price >= page.Filter.Min &&
                                                           page.Filter.Max != null && x.Price <= page.Filter.Max);

            if (page.Filter.Types is not null)
            {
                printingEditions = printingEditions.Where(x => page.Filter.Types.Contains(x.Type));
            }
            if (page.Filter.Sort is null)
            {
                paged = await printingEditions.ToPagedListAsync(page.PageNumber, page.PageSize);
                return paged;
            }
            if (page.Filter.Sort.Value)
            {
                printingEditions = printingEditions.OrderByDescending(pe => pe.Price);
            }
            if (!page.Filter.Sort.Value)
            {
                printingEditions = printingEditions.OrderBy(pe => pe.Price);
            }

            paged = await printingEditions.ToPagedListAsync(page.PageNumber, page.PageSize);
            return paged;
        }

        public new async Task<PrintingEdition> AddAsync(PrintingEdition item)
        {
            await _set.AddAsync(item);
            await SaveChangesAsync();

            var added = await GetAsync(item.Id);
            return added;
        }
    }
}
