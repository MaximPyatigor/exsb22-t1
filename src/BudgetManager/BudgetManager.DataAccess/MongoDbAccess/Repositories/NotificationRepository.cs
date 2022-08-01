using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }

        public async Task<IEnumerable<Notification>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var filter = Builders<Notification>.Filter.Eq(x => x.UserId, userId);
            var result = _collection.Find(filter).SortBy(x => x.NotificationDate).ToEnumerable(cancellationToken);
            return result;
        }
    }
}
