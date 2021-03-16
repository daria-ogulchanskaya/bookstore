using Bookstore.Data.Entities;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using System.Threading.Tasks;
using X.PagedList;

namespace Bookstore.Data.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IRepository<PrintingEdition>
    {
        Task<IPagedList<PrintingEdition>> GetPageAsync(PaginationRequest<PrintingEditionFilter> request);

        new Task<PrintingEdition> AddAsync(PrintingEdition item);
    }
}
