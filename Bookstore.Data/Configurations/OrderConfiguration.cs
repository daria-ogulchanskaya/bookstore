using Bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Payment);
            builder.HasOne(x => x.User);

            builder.HasMany(x => x.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(x => x.OrderId);

            builder.ToTable("Orders");
        }
    }
}
