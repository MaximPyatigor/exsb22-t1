using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(IMongoDbSettings settings, IMongoClient client)
            : base(settings, client)
        {
        }

        public async Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var filter = Builders<Wallet>.Filter.Eq(x => x.UserId, userId);
            var result = _collection.Find(filter).SortBy(x => x.DateOfChange).ToEnumerable(cancellationToken);
            return result;
        }
    }
}
