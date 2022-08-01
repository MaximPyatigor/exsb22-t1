using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;

namespace BudgetManager.DataAccess.MongoDbAccess.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
