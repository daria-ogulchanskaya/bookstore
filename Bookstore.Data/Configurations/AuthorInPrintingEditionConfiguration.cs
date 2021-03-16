using Bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Data.Configurations
{
    public class AuthorInPrintingEditionConfiguration : IEntityTypeConfiguration<AuthorInPrintingEdition>
    {
        public void Configure(EntityTypeBuilder<AuthorInPrintingEdition> builder)
        {
            builder.HasKey(x => new { x.AuthorId, x.PrintingEditionId });

            builder.HasOne(item => item.Author)
                   .WithMany(item => item.PrintingEditions)
                   .HasForeignKey(item => item.AuthorId);

            builder.HasOne(item => item.PrintingEdition)
                   .WithMany(item => item.Authors)
                   .HasForeignKey(item => item.PrintingEditionId);

            builder.ToTable("AuthorInBooks");
        }
    }
}
