using BudgetManager.CQRS.Responses.CategoryResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.CategoryQueries
{
    public record GetCategoriesQuery() : IRequest<IEnumerable<CategoryResponse>>;
}
