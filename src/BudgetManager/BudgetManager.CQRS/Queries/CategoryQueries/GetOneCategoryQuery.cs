using BudgetManager.CQRS.Responses.CategoryResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.CategoryQueries
{
    public record GetOneCategoryQuery(Guid id) : IRequest<CategoryResponse>;
}
