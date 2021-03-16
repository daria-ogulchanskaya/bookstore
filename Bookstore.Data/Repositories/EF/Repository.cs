using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories.EF
{
    public class Repository<T> : IRepository<T>
           where T : Entity
    {
        private readonly ApplicationContext _database;
        protected readonly DbSet<T> _set;

        public Repository(ApplicationContext databaseContext)
        {
            _database = databaseContext;
            _set = _database.Set<T>();
        }

        public virtual async Task<T> GetAsync(Guid id) =>
            await _set.FindAsync(id);

        public virtual async Task<IEnumerable<T>> AllAsync() =>
            await _set.ToListAsync();

        public async Task AddAsync(T item)
        {
            await _set.AddAsync(item);
            await _database.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<T> items)
        {
            await _set.AddRangeAsync(items);
            await _database.SaveChangesAsync();
        }

        public virtual async Task<Guid> RemoveAsync(Guid id)
        {
            var item = await _set.FindAsync(id);
            _set.Remove(item);

            await _database.SaveChangesAsync();

            return id;
        }

        public virtual async Task RemoveAsync(IEnumerable<T> items)
        {
            _set.RemoveRange(items);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            _database.Entry(item).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) =>
            !(await _set.FirstOrDefaultAsync(x => x.Id == id) is null);

        public async Task SaveChangesAsync() => 
            await _database.SaveChangesAsync();
    }
}
