using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.EF;
using Bookstore.Data.Repositories.Interfaces;

namespace Bookstore.Data.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationContext databaseContext)
            : base(databaseContext)
        { }
    }
}
