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

        public async Task<IEnumerable<Transaction>> GetTopElements(FilterDefinition<Transaction> filterDefinition, SortDefinition<Transaction> sortDefinition, int count, CancellationToken cancellationToken)
        {
            return await _collection.Find(filterDefinition).Sort(sortDefinition).Limit(count).ToListAsync(cancellationToken);
        }

        public async Task<bool> DeleteManyByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var fillter = Builders<Transaction>.Filter.Eq(x => x.UserId, userId);
            var result = await _collection.DeleteManyAsync(fillter, cancellationToken);
            return result is not null;
        }

        public async Task<bool> DeleteManyAsync(FilterDefinition<Transaction> filterDefinition, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteManyAsync(filterDefinition, cancellationToken);
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

        public async Task<(IEnumerable<Transaction>, long)> GetPageListAsync(FilterDefinition<Transaction> filterDefinition,
            SortDefinition<Transaction> sortDefinition,  int page, int pageSize, CancellationToken cancellationToken)
        {
            var filtred = _collection.Find(filterDefinition);

            var count = await filtred.CountAsync(cancellationToken);
            var result = await filtred.Sort(sortDefinition)
                .Skip(pageSize * (page - 1)).Limit(pageSize).ToListAsync(cancellationToken);

            return (result, count);
         }

        public async Task<IEnumerable<Transaction>> GetListByWalletIdAsync(Guid walletId, CancellationToken cancellationToken)
        {
            var filter = Builders<Transaction>.Filter.Eq(x => x.WalletId, walletId);
            var result = await _collection.Find(filter).SortBy(x => x.DateOfTransaction).ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetListByUserIdAndSubCategoryIdAsync(Guid userId, Guid subCategoryId, CancellationToken cancellationToken)
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.UserId, userId) & Builders<Transaction>.Filter.Eq(t => t.SubCategoryId, subCategoryId);
            var result = await _collection.Find(filter).SortBy(t => t.DateOfTransaction).ToListAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<Guid>> GetWalletDistinctCategoryIdListAsync(FilterDefinition<Transaction> filterDefinition,
            CancellationToken cancellationToken)
        {
            var result = await _collection.Distinct(x => x.CategoryId, filterDefinition, null, cancellationToken).ToListAsync();

            return result;
        }
    }
}
