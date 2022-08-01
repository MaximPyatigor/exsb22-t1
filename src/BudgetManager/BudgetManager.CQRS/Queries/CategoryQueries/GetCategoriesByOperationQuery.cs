using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using MediatR;

namespace BudgetManager.CQRS.Queries.CategoryQueries
{
    public record GetCategoriesByOperationQuery(Guid userId, OperationType operationType) : IRequest<IEnumerable<CategoryResponse>>;
}
