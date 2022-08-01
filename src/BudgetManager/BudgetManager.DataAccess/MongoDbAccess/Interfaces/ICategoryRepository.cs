using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;

namespace BudgetManager.DataAccess.MongoDbAccess.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetListByOperationAsync(Guid userId, OperationType operationType, CancellationToken cancellationToken);
        Task<IEnumerable<Category>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
