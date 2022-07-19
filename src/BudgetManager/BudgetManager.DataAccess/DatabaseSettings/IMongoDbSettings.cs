namespace BudgetManager.DataAccess.DatabaseSettings
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
