using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        public CountryRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }
    }
}
