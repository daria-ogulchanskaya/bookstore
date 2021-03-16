using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Shared.Models.Filter
{
    public class PrintingEditionFilter
    {
        public PrintingEditionType[] Types { get; set; }

        public double? Min { get; set; }

        public double? Max { get; set; }

        public CurrencyType? Currency { get; set; }

        public bool? Sort { get; set; }
    }
}
