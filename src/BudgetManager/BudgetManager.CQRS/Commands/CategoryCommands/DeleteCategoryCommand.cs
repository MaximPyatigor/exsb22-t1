using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CategoryCommands
{
    public record DeleteCategoryCommand(DeleteOneCategoryDTO deleteDto) : IRequest<bool>;
}
