using System;
using System.Threading.Tasks;
using Bookstore.Core.Models.Users;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => 
            _userService = userService;

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("all")]
        public IActionResult All() =>
            Ok(_userService.All());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id) =>
            Ok(await _userService.GetAsync(id));

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetPageAsync([FromQuery] PaginationRequest<UserFilter> request) =>
            Ok(await _userService.GetPageAsync(request));

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid id) =>
            Ok(await _userService.RemoveAsync(id));

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(UserModel user) =>
            Ok(await _userService.UpdateAsync(user));

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("change-block-status")]
        public async Task<IActionResult> ChangeBlockStatusAsync(Guid id) =>
            Ok(await _userService.ChangeBlockStatusAsync(id));
     }
}
