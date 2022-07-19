namespace BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
