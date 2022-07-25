
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record DeleteCategoryCommand(Guid id) : IRequest<bool>;
}
