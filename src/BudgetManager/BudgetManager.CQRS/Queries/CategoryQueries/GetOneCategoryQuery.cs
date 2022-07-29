using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Queries.CategoryQueries
{
    public record GetOneCategoryQuery(GetOneCategoryDTO queryDto) : IRequest<CategoryResponse>;
}
