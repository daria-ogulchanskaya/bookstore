using Bookstore.Core.Models.PrintingEdition;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Data.Entities;
using static Bookstore.Shared.Enums.Enums;
using Bookstore.Core.Exceptions;
using System.Net;
using static Bookstore.Shared.Constants.Constants.AppSettings;
using AutoMapper;
using Bookstore.Core.Models.Authors;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using Bookstore.Core.Providers;

namespace Bookstore.Core.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IAuthorInPrintingEditionRepository _authorInPrintsRepository;
        private readonly IMapper _mapper;
        private readonly CurrencyConverter _converter;

        public PrintingEditionService(IAuthorInPrintingEditionRepository authorInPrintsRepository,
                                      IPrintingEditionRepository printingEditionRepository,
                                      IMapper mapper,
                                      CurrencyConverter converter)
        {
            _authorInPrintsRepository = authorInPrintsRepository;
            _printingEditionRepository = printingEditionRepository;
            _mapper = mapper;
            _converter = converter;
        }

        public async Task<IEnumerable<PrintingEditionModel>> AllAsync()
        {
            var printingEditions = await _printingEditionRepository.AllAsync();

            var mapped = _mapper.Map<IEnumerable<PrintingEditionModel>>(printingEditions);

            return mapped;
        }

        public async Task<PrintingEditionModel> GetAsync(Guid id)
        {
            var printingEdition = await _printingEditionRepository.GetAsync(id);
            if (printingEdition is null)
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.PRINTING_EDITION_NOT_FOUND);
            }

            var mapped = _mapper.Map<PrintingEditionModel>(printingEdition);

            return mapped;
        }

        public async Task<PagedResponse<PrintingEditionModel>> GetPageAsync(PaginationRequest<PrintingEditionFilter> page)
        {
            var printingEditions = await _printingEditionRepository.GetPageAsync(page);
            var mapped = _mapper.Map<IEnumerable<PrintingEditionModel>>(printingEditions);

            var response = new PagedResponse<PrintingEditionModel>
            {
                Data = mapped,
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                HasNextPage = printingEditions.HasNextPage,
                HasPreviousPage = printingEditions.HasPreviousPage
            };

            if (page.Filter is null || page.Filter.Currency is null)
            {
                return response;
            }

            foreach (var item in mapped)
            {
                item.Price = _converter.Convert(item.Price, item.Currency, page.Filter.Currency);
                item.Currency = (CurrencyType)page.Filter.Currency;
            }

            return response;
        }

        public async Task<PrintingEditionModel> AddAsync(AddOrUpdatePrintingEditionModel model)
        {
            if (model is null)
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.EMPTY_PRINTING_EDITION);
            }

            var printingEditionModel = _mapper.Map<PrintingEditionModel>(model);

            printingEditionModel.Authors = model.Authors
                .Select(x => new AuthorInPrintingEditionModel(new Guid(x), printingEditionModel.Id));

            var converted = ConvertCurrencyToUSD(printingEditionModel);
 
            var printingEdition = _mapper.Map<PrintingEdition>(converted);

            if (await _printingEditionRepository.ExistsAsync(printingEdition.Id))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.PRINTING_EDITION_ALREADY_EXISTS);
            }

            var added = await _printingEditionRepository.AddAsync(printingEdition);

            var mapped = _mapper.Map<PrintingEditionModel>(added);

            return mapped;
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            if (!await _printingEditionRepository.ExistsAsync(id))
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.PRINTING_EDITION_NOT_FOUND);
            }

            var removed = await _printingEditionRepository.RemoveAsync(id);

            return removed;
        }

        public async Task<PrintingEditionModel> UpdateAsync(AddOrUpdatePrintingEditionModel model)
        {
            var printingEditionModel = _mapper.Map<PrintingEditionModel>(model);

            printingEditionModel.Authors = model.Authors
                .Select(x => new AuthorInPrintingEditionModel(new Guid(x), printingEditionModel.Id));

            var converted = ConvertCurrencyToUSD(printingEditionModel);

            if (model is null)
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.EMPTY_PRINTING_EDITION);
            }
            if (!await _printingEditionRepository.ExistsAsync(converted.Id))
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.PRINTING_EDITION_NOT_FOUND);
            }

            var printingEdition = _mapper.Map<PrintingEdition>(converted);

            await _printingEditionRepository.UpdateAsync(printingEdition);
            await _authorInPrintsRepository.UpdateAuthorsInPrintingEditionAsync(printingEdition.Authors, converted.Id);

            var updated = await _printingEditionRepository.GetAsync(printingEdition.Id);

            var mapped = _mapper.Map<PrintingEditionModel>(updated);

            return mapped;
        }

        public PrintingEditionModel ConvertCurrencyToUSD(PrintingEditionModel model)
        {
            if (model.Currency != CurrencyType.USD)
            {
                model.Price = _converter.Convert(model.Price, model.Currency, CurrencyType.USD);
                model.Currency = CurrencyType.USD;
            }

            return model;
        }
    }
}
