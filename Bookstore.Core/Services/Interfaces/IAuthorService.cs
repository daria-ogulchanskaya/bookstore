using Bookstore.Core.Models.Authors;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Page;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Core.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorModel> GetAsync(Guid id);

        public Task<PagedResponse<AuthorModel>> GetPageAsync(PaginationRequest request);

        public Task<IEnumerable<AuthorModel>> AllAsync();

        public Task<AuthorModel> AddAsync(AuthorModel model);

        public Task<Guid> RemoveAsync(Guid id);

        public Task<AuthorModel> UpdateAsync(AuthorModel model);
    }
}
