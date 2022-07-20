using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }
    }
}
