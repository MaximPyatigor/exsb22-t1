using BudgetManager.CQRS.Responses.CategoryResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.CategoryQueries
{
    public record GetCategoriesQuery(Guid userId) : IRequest<IEnumerable<CategoryResponse>>;
}
