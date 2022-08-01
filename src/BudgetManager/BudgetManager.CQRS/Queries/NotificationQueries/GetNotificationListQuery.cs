using BudgetManager.CQRS.Responses.NotificationResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.NotificationQueries
{
    public record GetNotificationListQuery(Guid userId) : IRequest<IEnumerable<NotificationResponse>>;
}
