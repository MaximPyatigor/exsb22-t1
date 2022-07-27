using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record AddCategoryCommand(AddCategoryDTO category) : IRequest<Guid>;
}
