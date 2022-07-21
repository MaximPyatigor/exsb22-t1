using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.SDK;
using MediatR;

namespace BudgetManager.CQRS.Commands.NotificationCommands
{
    public record AddNotificationCommand(AddNotificationDto notificationDto) : IRequest<string>;
}
