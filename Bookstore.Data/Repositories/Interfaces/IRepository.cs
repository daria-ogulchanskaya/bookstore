using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);

        Task<IEnumerable<T>> AllAsync();

        Task AddAsync(T item);

        Task AddAsync(IEnumerable<T> items);

        Task<Guid> RemoveAsync(Guid id);

        Task UpdateAsync(T item);

        Task<bool> ExistsAsync(Guid id);

        Task RemoveAsync(IEnumerable<T> items);
    }
}
