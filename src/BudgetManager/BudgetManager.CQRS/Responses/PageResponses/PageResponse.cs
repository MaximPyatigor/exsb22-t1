using BudgetManager.SDK.Pagination;

namespace BudgetManager.CQRS.Responses.PageResponses
{
    public class PageResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
