using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record DeleteCategoryCommand(Guid userId, Guid categoryId) : IRequest<bool>;
}
