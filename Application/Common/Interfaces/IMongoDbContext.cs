using MongoDB.Driver;

namespace Application.Common.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string Tname);
    }
}