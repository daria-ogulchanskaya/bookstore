using AutoMapper;
using Bookstore.Core.Models.PrintingEdition;
using Bookstore.Data.Entities;
using System;
using System.Linq;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Core.Mappers
{
    public class PrintingEditionMapper : Profile
    {
        public PrintingEditionMapper()
        {
            CreateMap<PrintingEdition, PrintingEditionModel>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<PrintingEditionModel, PrintingEdition>()
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));
            CreateMap<AddOrUpdatePrintingEditionModel, PrintingEditionModel>()
                .ForMember(x => x.Authors, opt => opt.Ignore())
                .ForMember(a => a.Id, opt => opt.MapFrom(m => m.Id == Guid.Empty ? Guid.NewGuid() : m.Id));

            CreateMap<string, CurrencyType>().ConvertUsing<CurrencyConverter>();
            CreateMap<string, PrintingEditionType>().ConvertUsing<PrintTypeConverter>();
        }

        public class CurrencyConverter : ITypeConverter<string, CurrencyType>
        {
            public CurrencyType Convert(string source, CurrencyType destination,
                ResolutionContext context) =>
                (CurrencyType)int.Parse(source);
        }
        public class PrintTypeConverter : ITypeConverter<string, PrintingEditionType>
        {
            public PrintingEditionType Convert(string source, PrintingEditionType destination, 
                ResolutionContext context) =>
                (PrintingEditionType)int.Parse(source);
        }
    }
}
