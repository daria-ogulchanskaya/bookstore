using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookstore.Core.Models.Users;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;

namespace Bookstore.Core.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserModel> GetAsync(Guid id);

        public Task<UserModel> GetAsync(string email);

        public Task<PagedResponse<UserModel>> GetPageAsync(PaginationRequest<UserFilter> request);

        public List<UserModel> All();

        public Task<Guid> RemoveAsync(Guid id);

        public Task<UserModel> UpdateAsync(UserModel user);

        public Task<UserModel> ChangeBlockStatusAsync(Guid id);

        public Task<bool> Exists(Guid id);
    }
}
