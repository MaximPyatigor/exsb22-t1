using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;

namespace BudgetManager.DataAccess.MongoDbAccess.Interfaces
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
