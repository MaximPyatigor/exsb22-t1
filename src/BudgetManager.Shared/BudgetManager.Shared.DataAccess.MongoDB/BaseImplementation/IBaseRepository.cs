using System.Linq.Expressions;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public interface IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        Task<List<TDocument>> GetAllAsync();

        IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        Task InsertOneAsync(TDocument document);
        Task InsertManyAsync(List<TDocument> documents);

        Task<TDocument> FindByIdAsync(Guid id);

        Task ReplaceOneAsync(TDocument document);

        Task DeleteByIdAsync(Guid id);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}
