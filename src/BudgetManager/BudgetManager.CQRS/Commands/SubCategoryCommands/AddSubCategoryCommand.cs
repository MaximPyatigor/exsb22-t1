using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.SubCategoryCommands
{
    public record AddSubCategoryCommand(AddSubCategoryDTO categoryObject, Guid userId) : IRequest<Guid>;
}
