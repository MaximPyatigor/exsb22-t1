using System.Linq.Expressions;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public interface IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        IQueryable<TDocument> AsQueryable();
        Task DeleteByIdAsync(Guid id);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
        Task<TDocument> FindByIdAsync(Guid id);
        Task InsertOneAsync(TDocument document);
        Task ReplaceOneAsync(TDocument document);
    }
}
