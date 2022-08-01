using System.Linq.Expressions;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Driver;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public interface IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        Task<IEnumerable<TDocument>> GetAllAsync(CancellationToken cancellationToken);

        IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression, CancellationToken cancellationToken);
        IEnumerable<TProjected> FilterBy<TProjected>(
        FilterDefinition<TDocument> filterDefinition,
        ProjectionDefinition<TDocument, TProjected> projectDefinition, CancellationToken cancellationToken);
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken);
        IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterDefenition, CancellationToken cancellationToken);
        Task InsertOneAsync(TDocument document, CancellationToken cancellationToken);
        Task InsertManyAsync(IEnumerable<TDocument> documents, CancellationToken cancellationToken);

        Task<TDocument> FindByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<TDocument> ReplaceOneAsync(TDocument document, CancellationToken cancellationToken);
        Task<TDocument> UpdateOneAsync(Expression<Func<TDocument, bool>> filterExpression,
            UpdateDefinition<TDocument> updateDefinition, CancellationToken cancellationToken);
        Task<TDocument> UpdateOneAsync(FilterDefinition<TDocument> filterExpression,
            UpdateDefinition<TDocument> updateDefinition, CancellationToken cancellationToken);

        Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken);
    }
}
