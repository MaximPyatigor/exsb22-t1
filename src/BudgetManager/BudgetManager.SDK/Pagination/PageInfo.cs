namespace BudgetManager.SDK.Pagination
{
    public class PageInfo
    {
        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasNextPage => PageNumber < TotalPages;

        public PageInfo(long count, int pageNumber, int pageSize = 10)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
