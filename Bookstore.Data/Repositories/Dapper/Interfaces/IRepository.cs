using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories.Dapper.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);

        Task<IEnumerable<T>> AllAsync();

        Task<int> AddAsync(T item);

        Task<bool> RemoveAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> ExistsAsync(Guid id);
    }
}
