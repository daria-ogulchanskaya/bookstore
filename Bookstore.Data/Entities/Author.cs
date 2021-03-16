using System.Collections.Generic;

namespace Bookstore.Data.Entities
{
    public class Author : Entity
    {
        public string Name { get; set; }

        public List<AuthorInPrintingEdition> PrintingEditions { get; set; }

        public Author() => 
            PrintingEditions = new List<AuthorInPrintingEdition>();

        public Author(string name) : this() => 
            Name = name;
    }
}
