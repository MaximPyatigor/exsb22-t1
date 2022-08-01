using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;

namespace BudgetManager.DataAccess.MongoDbAccess.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetListByOperationAsync(Guid userId, OperationType operationType, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
