namespace WebApi2Book.Data
{
    public class PagedDataRequest
    {
        public PagedDataRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool ExcludeLinks { get; set; }
    }
}