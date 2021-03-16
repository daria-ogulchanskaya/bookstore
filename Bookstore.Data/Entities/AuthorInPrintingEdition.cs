using Dapper.Contrib.Extensions;
using System;

namespace Bookstore.Data.Entities
{
    [Table("AuthorInBooks")]
    public class AuthorInPrintingEdition : Entity
    {
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        public Guid PrintingEditionId { get; set; }

        public PrintingEdition PrintingEdition { get; set; }

        public AuthorInPrintingEdition()
        {
        }

        public AuthorInPrintingEdition(Guid authorId, Guid printingEditionId)
        {
            AuthorId = authorId;
            PrintingEditionId = printingEditionId;
        }
    }
}
