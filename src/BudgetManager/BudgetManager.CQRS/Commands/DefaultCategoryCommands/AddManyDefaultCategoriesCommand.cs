using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.DefaultCategoryCommands
{
    public record AddManyDefaultCategoriesCommand(IEnumerable<DefaultCategory> defaultCategories) : IRequest<IEnumerable<Guid>>;
}
