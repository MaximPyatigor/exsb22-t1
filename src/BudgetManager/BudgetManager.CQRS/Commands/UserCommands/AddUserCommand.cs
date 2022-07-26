using BudgetManager.SDK.DTO.UserDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record AddUserCommand(AddUserDTO user) : IRequest<Guid>;
}
