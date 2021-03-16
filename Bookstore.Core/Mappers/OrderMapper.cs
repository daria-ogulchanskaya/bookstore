using Bookstore.Core.Models.Orders;
using Bookstore.Data.Entities;
using AutoMapper;
using System;

namespace Bookstore.Core.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<OrderModel, Order>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));

            CreateMap<OrderItem, OrderItemModel>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<OrderItemModel, OrderItem>()
                .ForMember(o => o.PrintingEdition, opt => opt.Ignore())
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
        }
    }
}
