namespace Bookstore.Core.Extensions
{
    public static class LongExtensions
    {
        public static long AsCents(this long amount) =>
            amount * 100;
    }
}
