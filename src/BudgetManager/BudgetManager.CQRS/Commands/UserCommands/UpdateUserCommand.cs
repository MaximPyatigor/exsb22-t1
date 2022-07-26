using BudgetManager.Model;
using BudgetManager.SDK.DTO.UserDTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record UpdateUserCommand(UpdateUserDTO updateUser) : IRequest<User>;
}
