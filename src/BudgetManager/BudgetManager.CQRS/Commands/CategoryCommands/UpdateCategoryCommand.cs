using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record UpdateCategoryCommand(UpdateCategoryDTO updateCategoryObject) : IRequest<CategoryResponse>;
}
