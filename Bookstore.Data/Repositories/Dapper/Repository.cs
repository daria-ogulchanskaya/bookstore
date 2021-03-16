using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Dapper.Interfaces;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace Bookstore.Data.Repositories.Dapper
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IConfiguration _configuration;
        protected readonly string _connectionString;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            using var dbContext = new SqlConnection(_connectionString);
            dbContext.Open();

            return await dbContext.GetAllAsync<T>();
        }

        public async Task<T> GetAsync(Guid id)
        {
            using var dbContext = new SqlConnection(_connectionString);
            dbContext.Open();

            return await dbContext.GetAsync<T>(id);
        }

        public async Task<int> AddAsync(T item)
        {
            using var dbContext = new SqlConnection(_connectionString);
            dbContext.Open();

            return await dbContext.InsertAsync(item);
        }

        public async Task<bool> RemoveAsync(T item)
        {
            using var dbContext = new SqlConnection(_connectionString);
            dbContext.Open();

            return await dbContext.DeleteAsync(item);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            using var dbContext = new SqlConnection(_connectionString);
            dbContext.Open();

            return await dbContext.UpdateAsync(item);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            using var dbContext = new SqlConnection(_connectionString);
            dbContext.Open();

            var entity = await dbContext.GetAsync<T>(id);

            return !(entity is null);
        }

    }
}
