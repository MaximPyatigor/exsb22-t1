using Application.Common.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.DbContext
{
    public class MongoDbContext : IMongoDbContext
    {
        readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoDbConfig> _databaseSettings)
        {
            var _mongoClient = new MongoClient(_databaseSettings.Value.ConnectionString);

            _database = _mongoClient.GetDatabase(_databaseSettings.Value.Name);
        }
        public IMongoCollection<T> GetCollection<T>(string Tname) => _database.GetCollection<T>(Tname);
    }
}