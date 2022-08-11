using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MongoDB.Driver;

namespace BudgetManager.DataAccess.MongoDbAccess.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetListByOperationAsync(Guid userId, OperationType operationType, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> DeleteManyByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<(IEnumerable<Transaction>, long)> GetPageListByOperationAsync(Guid userId, OperationType operationType, int page, int pageSize, CancellationToken cancellationToken);
        Task<(IEnumerable<Transaction>, long)> GetPageListAsync(FilterDefinition<Transaction> filterDefinition,
            SortDefinition<Transaction> sortDefinition, int page, int pageSize, CancellationToken cancellationToken);
    }
}
