using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.EF;
using Bookstore.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories
{
    public class AuthorInPrintingEditionRepository : Repository<AuthorInPrintingEdition>, 
        IAuthorInPrintingEditionRepository
    {
        public AuthorInPrintingEditionRepository(ApplicationContext databaseContext)
            : base(databaseContext)
        { }

        public async Task UpdateAuthorsInPrintingEditionAsync(IEnumerable<AuthorInPrintingEdition> list, Guid printingEditionId)
        {
            var items = _set.Where(x => x.PrintingEditionId == printingEditionId);

            await RemoveAsync(items);
            await AddAsync(list);
        }
    }
}
