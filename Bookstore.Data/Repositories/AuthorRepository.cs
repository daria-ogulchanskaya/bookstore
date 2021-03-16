using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Interfaces;
using System.Linq;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using Bookstore.Shared.Models.Page;
using Bookstore.Data.Repositories.EF;
using static Microsoft.EntityFrameworkCore.EF;

namespace Bookstore.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<IPagedList<Author>> GetPageAsync(PaginationRequest request)
        {
            IQueryable<Author> set = _set.Include(author => author.PrintingEditions)
                                         .ThenInclude(authorInPrint => authorInPrint.PrintingEdition);

            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                set = set.Where(x => Functions.Like(x.Name.ToLower(), $"%{request.SearchText.ToLower()}%"));
            }

            var paged = await set.ToPagedListAsync(request.PageNumber, request.PageSize);
            return paged;
        }

        public override async Task<IEnumerable<Author>> AllAsync()
        {
            return await _set.Include(author => author.PrintingEditions)
                             .ThenInclude(authorInPrint => authorInPrint.PrintingEdition)
                             .ToListAsync();
        }

        public override async Task<Author> GetAsync(Guid id)
        {
            return await _set.Include(author => author.PrintingEditions)
                             .ThenInclude(authorInPrint => authorInPrint.PrintingEdition)
                             .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
