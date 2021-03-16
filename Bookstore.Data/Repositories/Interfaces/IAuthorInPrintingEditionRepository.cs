using Bookstore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories.Interfaces
{
    public interface IAuthorInPrintingEditionRepository : 
        IRepository<AuthorInPrintingEdition>
    {
        public Task UpdateAuthorsInPrintingEditionAsync(IEnumerable<AuthorInPrintingEdition> list, Guid printingEditionId);
    }
}
