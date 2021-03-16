using Bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Data.Configurations
{
    public class PrintingEditionConfiguration : IEntityTypeConfiguration<PrintingEdition>
    {
        public void Configure(EntityTypeBuilder<PrintingEdition> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Authors)
                   .WithOne(x => x.PrintingEdition);

            builder.ToTable("PrintingEditions");
        }
    }
}
