namespace BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
