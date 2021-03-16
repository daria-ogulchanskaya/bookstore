using Bookstore.Data.Entities;
using System.Collections.Generic;

namespace Bookstore.Core.Models.Authors
{
    public class AuthorModel : Model
    {
        public string Name { get; set; }

        public List<AuthorInPrintingEdition> PrintingEditions { get; set;}

        public AuthorModel() =>
            PrintingEditions = new List<AuthorInPrintingEdition>();
}
}
