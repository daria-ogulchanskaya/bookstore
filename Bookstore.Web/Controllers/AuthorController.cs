using Bookstore.Core.Models.Authors;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Shared.Models.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bookstore.Web.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService) => 
            _authorService = authorService;

        [HttpGet("all")]
        public async Task<IActionResult> AllAsync() =>
            Ok(await _authorService.AllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id) =>
            Ok(await _authorService.GetAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetPageAsync([FromQuery] PaginationRequest request) =>
            Ok(await _authorService.GetPageAsync(request));

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AuthorModel author) =>
            Ok(await _authorService.AddAsync(author));

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid id) =>
            Ok(await _authorService.RemoveAsync(id));

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(AuthorModel author) =>
            Ok(await _authorService.UpdateAsync(author));
    }
}
