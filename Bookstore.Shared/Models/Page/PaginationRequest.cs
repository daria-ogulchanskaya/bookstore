namespace Bookstore.Shared.Models.Page
{
    public class PaginationRequest<T>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string SearchText { get; set; }

        public T Filter { get; set; }

        public PaginationRequest()
        {
            PageNumber = 1;
            PageSize = 6;
        }

        public PaginationRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class PaginationRequest
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string SearchText { get; set; }

        public PaginationRequest()
        {
            PageNumber = 1;
            PageSize = 6;
        }

        public PaginationRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
