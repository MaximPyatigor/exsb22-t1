using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record AddManyUsersCommand(IEnumerable<AddUserDTO> users) : IRequest<IEnumerable<Guid>>;
}
