using Bookstore.Core.Models.Authors;
using Bookstore.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Bookstore.Core.Models.PrintingEdition
{
    public class PrintingEditionModel : Model
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public Enums.CurrencyType Currency { get; set; }

        public Enums.PrintingEditionType Type { get; set; }

        public IEnumerable<AuthorInPrintingEditionModel> Authors { get; set; }

        public PrintingEditionModel() =>
            Authors = new List<AuthorInPrintingEditionModel>();

        public PrintingEditionModel(Guid id) : base(id) => 
            Authors = new List<AuthorInPrintingEditionModel>();
    }
}
