using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.NotificationCommands
{
    public record AddNotificationCommand(int Type, string Description, bool IsRead = false) : IRequest<NotificationResponse>;
}
