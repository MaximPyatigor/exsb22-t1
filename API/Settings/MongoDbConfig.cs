namespace API.Settings
{
    public class MongoDbConfig
    {
        public string DatabaseName { get; init; }
        public string ConnectionString { get; init; }
        public string TransactionsCollectionName { get; set; }
    }
}
