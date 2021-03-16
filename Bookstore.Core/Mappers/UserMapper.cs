using AutoMapper;
using Bookstore.Core.Models.Users;
using Bookstore.Data.Entities;
using System;

namespace Bookstore.Core.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserModel>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<UserModel, User>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
        }
    }
}
