
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Queries.NotificationQueries
{
    public record GetNotificationListQuery() : IRequest<IQueryable<Notification>;
}
