using System;
using Bookstore.Data.Configurations;
using Bookstore.Data.Entities;
using Bookstore.Data.Initialization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data.AppContext
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<AuthorInPrintingEdition> Authors { get; set; }

        public DbSet<AuthorInPrintingEdition> AuthorInBooks { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> Items { get; set; }

        public DbSet<PrintingEdition> PrintingEditions { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new AuthorInPrintingEditionConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new PrintingEditionConfiguration());

            DatabaseInitialization.Initialize(builder);
        }
    }
}
