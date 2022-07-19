using System.Linq.Expressions;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public interface IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        IQueryable<TDocument> AsQueryable();
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
        Task InsertOneAsync(TDocument document);
        Task<TDocument> FindByIdAsync(Guid id);
        Task ReplaceOneAsync(TDocument document);
        Task DeleteByIdAsync(Guid id);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}