using Bookstore.Core.Models;
using System;
using System.Collections.Generic;

namespace Bookstore.Core.Models.PrintingEdition
{
    public class AddOrUpdatePrintingEditionModel : Model
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }

        public List<string> Authors { get; set; }
    }
}
