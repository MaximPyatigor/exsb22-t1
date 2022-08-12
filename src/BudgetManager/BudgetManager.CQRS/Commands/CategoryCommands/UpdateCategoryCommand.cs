using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record UpdateCategoryCommand(Guid userId, Guid categoryId, UpdateCategoryDTO updateCategoryObject) : IRequest<CategoryResponse>;
}
