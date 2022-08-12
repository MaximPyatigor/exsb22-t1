using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record AddCategoryCommand(Guid userId, AddCategoryDTO category) : IRequest<Guid>;
}
