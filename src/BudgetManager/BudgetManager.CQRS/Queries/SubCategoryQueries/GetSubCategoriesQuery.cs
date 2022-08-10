using BudgetManager.CQRS.Responses.SubCategoryResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.SubCategoryQueries
{
    public record GetSubCategoriesQuery(Guid userId, Guid categoryId) : IRequest<IEnumerable<SubCategoryResponse>>;
}
