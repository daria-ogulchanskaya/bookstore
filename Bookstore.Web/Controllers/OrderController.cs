using Bookstore.Core.Models.Orders;
using Bookstore.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Core.Models.Payments;
using Bookstore.Shared.Models.Page;
using Bookstore.Shared.Models.Filter;
using Microsoft.AspNetCore.Http;

namespace Bookstore.Web.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _accessor;

        public OrderController(IOrderService orderService,
                               IHttpContextAccessor accessor)
        {
            _orderService = orderService;
            _accessor = accessor;
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("all")]
        public async Task<IActionResult> AllAsync() =>
            Ok(await _orderService.AllAsync());

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationRequest<OrderFilter> request) =>
            Ok(await _orderService.GetPageAsync(request));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id) =>
             Ok(await _orderService.GetAsync(id));

        [Authorize(Roles = "Client", AuthenticationSchemes = "Bearer")]
        [HttpGet("client/orders")]
        public async Task<IActionResult> GetUserOrdersAsync()
        {
            var id = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value;

            return Ok(await _orderService.GetUserOrdersAsync(new Guid(id)));
        }


        [Authorize(Roles = "Client", AuthenticationSchemes = "Bearer")]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(List<OrderItemModel> items)
        {
            var id = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value;

            return Ok(await _orderService.AddAsync(items, new Guid(id)));
        }

        [HttpDelete("remove/{id}")]
        public async Task RemoveAsync([FromRoute] Guid id) =>
            await _orderService.RemoveAsync(id);

        [Authorize(Roles = "Client", AuthenticationSchemes = "Bearer")]
        [HttpPost("charge")]
        public async Task ChargeAsync(PaymentModel payment)
        {
            var email = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Email").Value;

            await _orderService.ChargeAsync(payment, email);
        }

        [HttpDelete("delete/payment/{id}")]
        public async Task DeletePaymentAsync([FromRoute] Guid id) =>
            await _orderService.DeletePaymentAsync(id);
    }
}
