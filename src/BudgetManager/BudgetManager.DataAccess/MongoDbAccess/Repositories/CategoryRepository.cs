using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }

        public async Task<IEnumerable<Category>> GetListByOperationAsync(Guid userId, OperationType operationType, CancellationToken cancellationToken)
        {
            var filterUser = Builders<Category>.Filter.Eq(x => x.UserId, userId);
            var filterOperation = Builders<Category>.Filter.Eq(x => x.CategoryType, operationType);
            var filter = Builders<Category>.Filter.And(filterUser, filterOperation);
            var result = _collection.Find(filter).ToEnumerable(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Category>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var filter = Builders<Category>.Filter.Eq(x => x.UserId, userId);
            var result = _collection.Find(filter).ToEnumerable(cancellationToken);
            return result;
        }
    }
}
