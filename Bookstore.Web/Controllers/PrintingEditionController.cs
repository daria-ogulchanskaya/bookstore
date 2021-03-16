using Bookstore.Core.Models.PrintingEdition;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bookstore.Web.Controllers
{
    [ApiController]
    [Route("api/printing-editions")]
    public class PrintingEditionController : Controller
    {
        private readonly IPrintingEditionService _printingEditionService;

        public PrintingEditionController(IPrintingEditionService printingEditionService) =>
            _printingEditionService = printingEditionService;

        [HttpGet("all")]
        public async Task<IActionResult> AllAsync() =>
            Ok(await _printingEditionService.AllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id) =>
            Ok(await _printingEditionService.GetAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetPageAsync([FromQuery] PaginationRequest<PrintingEditionFilter> page) => 
            Ok(await _printingEditionService.GetPageAsync(page));

        [HttpPost("add")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddAsync([FromBody] AddOrUpdatePrintingEditionModel model) =>
            Ok(await _printingEditionService.AddAsync(model));

        [HttpDelete("remove/{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid id) =>
            Ok(await _printingEditionService.RemoveAsync(id));

        [HttpPost("update")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAsync([FromBody] AddOrUpdatePrintingEditionModel model) => 
            Ok(await _printingEditionService.UpdateAsync(model));
    }
}
