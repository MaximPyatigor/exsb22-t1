using BudgetManager.CQRS.Responses.DefaultCategoryResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.DefaultCategoryQueries
{
    public record GetDefaultCategoriesQuery : IRequest<IEnumerable<DefaultCategoryResponse>>;
}
