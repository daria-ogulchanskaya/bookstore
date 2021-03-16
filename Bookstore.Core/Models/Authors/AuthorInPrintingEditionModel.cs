using Bookstore.Core.Models.PrintingEdition;
using System;

namespace Bookstore.Core.Models.Authors
{
    public class AuthorInPrintingEditionModel : Model
    {
        public Guid AuthorId { get; set; }

        public AuthorModel Author { get; set; }

        public Guid PrintingEditionId { get; set; }

        public PrintingEditionModel PrintingEdition { get; set; }

        public AuthorInPrintingEditionModel() { }

        public AuthorInPrintingEditionModel(Guid authorId, Guid printingEditionId)
        {
            AuthorId = authorId;
            PrintingEditionId = printingEditionId;
        }
    }
}
