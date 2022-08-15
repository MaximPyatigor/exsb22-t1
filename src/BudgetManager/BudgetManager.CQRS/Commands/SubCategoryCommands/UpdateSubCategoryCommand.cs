using BudgetManager.CQRS.Responses.SubCategoryResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.SubCategoryCommands
{
    public record UpdateSubCategoryCommand(Guid userId, UpdateSubCategoryDTO updateSubCategoryDTO) : IRequest<SubCategoryResponse>;
}
