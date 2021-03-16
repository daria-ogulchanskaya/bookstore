using AutoMapper;
using Bookstore.Core.Exceptions;
using Bookstore.Core.Models.Orders;
using Bookstore.Core.Models.Payments;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Interfaces;
using Bookstore.Shared.Enums;
using Bookstore.Shared.Models;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using Bookstore.Shared.Models.Settings;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Bookstore.Shared.Constants.Constants.AppSettings;
using Order = Bookstore.Data.Entities.Order;
using Bookstore.Core.Extensions;

namespace Bookstore.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly StripeSettings _stripeSettings;

        public OrderService(IOrderRepository orderRepository,
                            IPaymentRepository paymentRepository,
                            IMapper mapper,
                            IUserService userService,
                            IOptions<StripeSettings> stripeSettings)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _userService = userService;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<OrderModel> GetAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            if (order is null)
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.ORDER_NOT_FOUND);
            }

            var mapped = _mapper.Map<OrderModel>(order);

            return mapped;
        }

        public async Task<PagedResponse<OrderModel>> GetPageAsync(PaginationRequest<OrderFilter> request)
        {
            var orders = await _orderRepository.GetPageAsync(request);

            var mapped = _mapper.Map<IEnumerable<OrderModel>>(orders);

            var response = new PagedResponse<OrderModel>
            {
                Data = mapped,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNextPage = orders.HasNextPage,
                HasPreviousPage = orders.HasPreviousPage
            };

            return response;
        }

        public async Task<IEnumerable<OrderModel>> AllAsync()
        {
            var orders = await _orderRepository.AllAsync();

            var mapped = _mapper.Map<IEnumerable<OrderModel>>(orders);

            return mapped;
        }

        public async Task<OrderModel> AddAsync(List<OrderItemModel> items, Guid id)
        {
            var user = await _userService.GetAsync(id);

            double totalCost = items.Sum(x => x.Count * x.PrintingEdition.Price);
            if (totalCost <= default(int))
            {
                throw new ServerException(HttpStatusCode.BadRequest, 
                    ExceptionMessage.INVALID_DATA);
            }

            var payment = new Payment()
            {
                TransactionId = string.Empty
            };

            var order = new OrderModel
            {
                UserId = user.Id,
                CreationDate = DateTime.Now,
                TotalCost = totalCost,
                Status = Enums.OrderStatusType.Unpaid,
                PaymentId = payment.Id,
                Description = string.Empty
            };

            foreach (var item in items)
            {
                item.OrderId = order.Id;
            }

            order.Items = items;

            var mapped = _mapper.Map<Order>(order);

            await _paymentRepository.AddAsync(payment);
            await _orderRepository.AddAsync(mapped);

            return order;
        }

        public async Task RemoveAsync(Guid id)
        {
            if (!await _orderRepository.ExistsAsync(id))
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.ORDER_NOT_FOUND);
            }

            await _orderRepository.RemoveAsync(id);
        }

        public async Task ChargeAsync(PaymentModel payment, string email)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            var customer = await customerService.CreateAsync(new CustomerCreateOptions()
            {
                Email = email,
                Source = payment.Token
            });

            long totalCost = (long)payment.Order.Items.Sum(x => x.Count * x.PrintingEdition.Price);
            if (totalCost <= default(int))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_DATA);
            }

            var charge = await chargeService.CreateAsync(new ChargeCreateOptions
            {
                Amount = totalCost.AsCents(),
                Description = payment.Order.Description,
                Currency = _stripeSettings.Currency,
                Customer = customer.Id,
                ReceiptEmail = email
            });

            if (charge.Status != _stripeSettings.SucceededChargeStatus)
            {
                throw new ServerException(HttpStatusCode.InternalServerError,
                    ExceptionMessage.PAYMENT_FAILED);
            }

            var orderPayment = await _paymentRepository.GetAsync(payment.Order.PaymentId);
            orderPayment.TransactionId = charge.BalanceTransactionId;
            await _paymentRepository.UpdateAsync(orderPayment);

            payment.Order.Status = Enums.OrderStatusType.Paid;
            var mapped = _mapper.Map<Order>(payment.Order);

            await _orderRepository.UpdateAsync(mapped);
        }

        public async Task<IEnumerable<OrderModel>> GetUserOrdersAsync(Guid id)
        {
            if (!await _userService.Exists(id))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                                  ExceptionMessage.USER_NOT_FOUND);
            }

            var orders = await _orderRepository.GetUserOrdersAsync(id);

            var mapped = _mapper.Map<IEnumerable<OrderModel>>(orders);

            return mapped;
        }

        public async Task DeletePaymentAsync(Guid id) => 
            await _paymentRepository.RemoveAsync(id);
    }
}
