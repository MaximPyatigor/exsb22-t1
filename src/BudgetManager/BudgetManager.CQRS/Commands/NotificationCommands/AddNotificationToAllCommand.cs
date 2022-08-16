using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.NotificationCommands
{
    public record AddNotificationToAllCommand(AddNotificationDto NotificationDto) : IRequest<Guid>;
}
