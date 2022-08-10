using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }

        public async Task<bool> DeleteManyByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var fillter = Builders<Transaction>.Filter.Eq(x => x.UserId, userId);
            var result = await _collection.DeleteManyAsync(fillter, cancellationToken);
            return result is not null;
        }

        public async Task<IEnumerable<Transaction>> GetListByOperationAsync(Guid userId, OperationType operationType, CancellationToken cancellationToken)
        {
            var filterUser = Builders<Transaction>.Filter.Eq(x => x.UserId, userId);
            var filterOperation = Builders<Transaction>.Filter.Eq(x => x.TransactionType, operationType);
            var filter = Builders<Transaction>.Filter.And(filterUser, filterOperation);
            var result = await _collection.Find(filter).SortBy(x => x.DateOfTransaction).ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var filter = Builders<Transaction>.Filter.Eq(x => x.UserId, userId);
            var result = await _collection.Find(filter).SortBy(x => x.DateOfTransaction).ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetListByWalletIdAsync(Guid walletId, CancellationToken cancellationToken)
        {
            var filter = Builders<Transaction>.Filter.Eq(x => x.WalletId, walletId);
            var result = await _collection.Find(filter).SortBy(x => x.DateOfTransaction).ToListAsync(cancellationToken);
            return result;
        }
    }
}
