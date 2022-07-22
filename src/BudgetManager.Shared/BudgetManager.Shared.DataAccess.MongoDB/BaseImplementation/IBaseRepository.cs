using System.Linq.Expressions;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public interface IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        IQueryable<TDocument> AsQueryable();

        IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        Task InsertOneAsync(TDocument document, CancellationToken cancellationToken);

        Task<TDocument> FindByIdAsync(Guid id, CancellationToken cancellationToken);

        Task ReplaceOneAsync(TDocument document);

        Task DeleteByIdAsync(Guid id);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}
