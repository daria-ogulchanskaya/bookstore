using System.Collections.Generic;

namespace Bookstore.Shared.Models
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data) => 
            Data = data;
    }
}
