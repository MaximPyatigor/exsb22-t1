using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }
    }
}
