using Bookstore.Core.Models.Users;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookstore.Core.Exceptions;
using static Bookstore.Shared.Constants.Constants.AppSettings;
using X.PagedList;
using AutoMapper;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using Bookstore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace Bookstore.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
                           IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserModel> GetAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.USER_NOT_FOUND);
            }

            var mapped = _mapper.Map<UserModel>(user);

            return mapped;
        }

        public async Task<UserModel> GetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.USER_NOT_FOUND);
            }

            var mapped = _mapper.Map<UserModel>(user);

            return mapped;
        }

        public async Task<PagedResponse<UserModel>> GetPageAsync(PaginationRequest<UserFilter> request)
        {
            var users = _userManager.Users;

            if (!(request.Filter is null))
            {
                users = users.Where(x => x.IsBlocked == request.Filter.BlockStatus);
            }
            if (!(request.SearchText is null))
            {
                var searchText = request.SearchText.ToLower();

                users = users.Where(user => Functions.Like(user.Name.ToLower(), $"%{searchText}%") ||
                                            Functions.Like(user.Surname.ToLower(), $"%{searchText}%") ||
                                            Functions.Like(user.PhoneNumber.ToLower(), $"%{searchText}%") ||
                                            Functions.Like(user.Email.ToLower(), $"%{searchText}%"));
            }

            var mapped = await _mapper.Map<IEnumerable<UserModel>>(users)
                .ToPagedListAsync(request.PageNumber, request.PageSize);

            var response = new PagedResponse<UserModel>
            {
                Data = mapped,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNextPage = mapped.HasNextPage,
                HasPreviousPage = mapped.HasPreviousPage
            };

            return response;
        }

        public List<UserModel> All()
        {
            var users = _userManager.Users;

            var mapped = _mapper.Map<IEnumerable<UserModel>>(users);

            return mapped.ToList();
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.USER_NOT_FOUND);
            }

            await _userManager.DeleteAsync(user);

            return id;
        }

        public async Task<UserModel> UpdateAsync(UserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.USER_NOT_FOUND);
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.UserName = model.Username;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user);

            var mapped = _mapper.Map<UserModel>(user);

            return mapped;
        }

        public async Task<UserModel> ChangeBlockStatusAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.BadRequest, 
                    ExceptionMessage.USER_NOT_FOUND);
            }

            user.IsBlocked = !user.IsBlocked;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ServerException(HttpStatusCode.BadRequest, 
                    result.Errors.ToString());
            }

            var mapped = _mapper.Map<UserModel>(user);

            return mapped;
        }

        public async Task<bool> Exists(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return user is not null;
        }
    }
}
