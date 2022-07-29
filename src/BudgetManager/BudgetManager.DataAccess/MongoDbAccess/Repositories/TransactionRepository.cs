using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }

        public async Task<IEnumerable<Transaction>> GetListByUserIdAsync(Guid userId, string operationType, CancellationToken cancellationToken)
        {
            var filterUser = Builders<Transaction>.Filter.Eq(x => x.UserId, userId);
            var filterOperation = Builders<Transaction>.Filter.Eq(x => x.TransactionType.ToString(), operationType);
            var filter = Builders<Transaction>.Filter.And(filterUser, filterOperation);
            var result = _collection.Find(filter).ToEnumerable(cancellationToken);
            return result;
        }
    }
}
