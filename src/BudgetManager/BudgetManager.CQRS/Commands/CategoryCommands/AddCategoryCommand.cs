using BudgetManager.SDK.DTO.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record AddCategoryCommand(AddCategoryDTO category) : IRequest<string>;
}
