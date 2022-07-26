using BudgetManager.Model;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record UpdateCategoryCommand(UpdateCategoryDTO updateCategoryObject) : IRequest<Category>;
}
