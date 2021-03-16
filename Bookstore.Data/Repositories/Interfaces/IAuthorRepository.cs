using Bookstore.Data.Entities;
using Bookstore.Shared.Models.Page;
using System.Threading.Tasks;
using X.PagedList;

namespace Bookstore.Data.Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IPagedList<Author>> GetPageAsync(PaginationRequest request);
    }
}
