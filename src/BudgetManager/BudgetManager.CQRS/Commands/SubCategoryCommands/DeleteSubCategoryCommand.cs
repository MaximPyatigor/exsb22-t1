using MediatR;

namespace BudgetManager.CQRS.Commands.SubCategoryCommands
{
    public record DeleteSubCategoryCommand(Guid userId, Guid categoryId, Guid subCategoryId) : IRequest<bool>;
}