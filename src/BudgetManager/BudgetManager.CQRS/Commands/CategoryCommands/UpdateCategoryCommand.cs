using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record UpdateCategoryCommand(Guid userId, UpdateCategoryDTO updateCategoryObject) : IRequest<CategoryResponse>;
}
