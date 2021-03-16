using AutoMapper;
using Bookstore.Core.Models.Authors;
using Bookstore.Data.Entities;
using System;

namespace Bookstore.Core.Mappers
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<Author, AuthorModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(a => a.Id == Guid.Empty ? Guid.NewGuid() : a.Id));
            CreateMap<AuthorModel, Author>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));


            CreateMap<AuthorInPrintingEdition, AuthorInPrintingEditionModel>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<AuthorInPrintingEditionModel, AuthorInPrintingEdition>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
        }
    }
}
