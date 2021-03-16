using Bookstore.Core.Models.Authors;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using static Bookstore.Shared.Constants.Constants.AppSettings;
using Bookstore.Core.Exceptions;
using AutoMapper;
using Bookstore.Data.Entities;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Page;

namespace Bookstore.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, 
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorModel>> AllAsync()
        {
            var authors = await _repository.AllAsync();

            var mapped = _mapper.Map<IEnumerable<AuthorModel>>(authors);

            return mapped;
        }

        public async Task<AuthorModel> GetAsync(Guid id)
        {
            var author = await _repository.GetAsync(id);
            if (author is null)
            {
                throw new ServerException(HttpStatusCode.NotFound, 
                    ExceptionMessage.AUTHOR_NOT_FOUND);
            }

            var mapped = _mapper.Map<AuthorModel>(author);

            return mapped;
        }

        public async Task<AuthorModel> AddAsync(AuthorModel model)
        {
            if (model is null)
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.EMPTY_AUTHOR);
            }
            if(await _repository.ExistsAsync(model.Id))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.AUTHOR_ALREADY_EXISTS);
            }

            var author = _mapper.Map<Author>(model);

            await _repository.AddAsync(author);

            var mapped = _mapper.Map<AuthorModel>(author);

            return mapped;
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            if(!await _repository.ExistsAsync(id))
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.AUTHOR_NOT_FOUND);
            }

            var removed = await _repository.RemoveAsync(id);

            return removed;
        }

        public async Task<AuthorModel> UpdateAsync(AuthorModel model)
        {
            if (model is null)
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.EMPTY_AUTHOR);
            }
            if (!await _repository.ExistsAsync(model.Id))
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.AUTHOR_NOT_FOUND);
            }

            var author = _mapper.Map<Author>(model);

            await _repository.UpdateAsync(author);

            var mapped = _mapper.Map<AuthorModel>(author);

            return mapped;
        }

        public async Task<PagedResponse<AuthorModel>> GetPageAsync(PaginationRequest request)
        {
            var authors = await _repository.GetPageAsync(request);

            var mapped = _mapper.Map<IEnumerable<AuthorModel>>(authors);

            var response = new PagedResponse<AuthorModel>
            {
                Data = mapped,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNextPage = authors.HasNextPage,
                HasPreviousPage = authors.HasPreviousPage
            };

            return response;
        }
    }
}
