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
        Task<IEnumerable<Transaction>> GetListByWalletIdAsync(Guid walletId, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetListByUserIdAndSubCategoryIdAsync(Guid userId, Guid subCategoryId, CancellationToken cancellationToken);
        Task<bool> DeleteManyByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<(IEnumerable<Transaction>, long)> GetPageListAsync(FilterDefinition<Transaction> filterDefinition,
            SortDefinition<Transaction> sortDefinition, int page, int pageSize, CancellationToken cancellationToken);
        Task<IEnumerable<Guid>> GetWalletDistinctCategoryIdListAsync(FilterDefinition<Transaction> filterDefinition,
            CancellationToken cancellationToken);
        Task<bool> DeleteManyAsync(FilterDefinition<Transaction> filterDefinition, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetTopElementsAsync(FilterDefinition<Transaction> filterDefinition, SortDefinition<Transaction> sortDefinition, int count, CancellationToken cancellationToken);
    }
}
