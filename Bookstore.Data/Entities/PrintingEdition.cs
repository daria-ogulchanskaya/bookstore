using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Data.Entities
{
    public class PrintingEdition : Entity
    {
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public double Price { get; set; }

        public CurrencyType Currency { get; set; }

        public PrintingEditionType Type { get; set; }

        public IEnumerable<AuthorInPrintingEdition> Authors { get; set; }

        public PrintingEdition() => 
            Authors = new List<AuthorInPrintingEdition>();
    }
}
