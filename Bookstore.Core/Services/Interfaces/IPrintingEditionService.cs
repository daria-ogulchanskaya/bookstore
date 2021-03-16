using Bookstore.Core.Models.PrintingEdition;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Core.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<PrintingEditionModel> GetAsync(Guid id);

        Task<PagedResponse<PrintingEditionModel>> GetPageAsync(PaginationRequest<PrintingEditionFilter> page);

        Task<IEnumerable<PrintingEditionModel>> AllAsync();

        Task<PrintingEditionModel> AddAsync(AddOrUpdatePrintingEditionModel model);

        Task<Guid> RemoveAsync(Guid id);

        Task<PrintingEditionModel> UpdateAsync(AddOrUpdatePrintingEditionModel model);
    }
}
