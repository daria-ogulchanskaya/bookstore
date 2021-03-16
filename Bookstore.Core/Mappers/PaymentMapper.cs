using AutoMapper;
using Bookstore.Core.Models.Payments;
using Bookstore.Data.Entities;
using System;

namespace Bookstore.Core.Mappers
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<Payment, PaymentModel>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<PaymentModel, Payment>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
        }
    }
}
