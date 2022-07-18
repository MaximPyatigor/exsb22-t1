using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BudgetManager.Shared.DataAccess.MongoDB.DbContexts
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase mongoDatabase;

        public MongoDbContext(IOptions<ExadelBudgetDatabaseSettings> dbSettings)
        {
            mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        }

        public IMongoCollection<TCollection> GetCollection<TCollection>(string name)
        {
            return mongoDatabase.GetCollection<TCollection>(name);
        }
    }
}
