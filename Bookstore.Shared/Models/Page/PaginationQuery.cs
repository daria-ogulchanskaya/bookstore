namespace Bookstore.Shared.Models
{
    public class PaginationQuery

    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 6;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
