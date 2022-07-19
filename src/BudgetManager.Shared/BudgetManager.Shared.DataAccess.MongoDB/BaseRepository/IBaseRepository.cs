using System.Linq.Expressions;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseRepository
{
    public interface IBaseRepository<TDocument> 
        where TDocument : class
    {
        Task DeleteByIdAsync(string id);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
        Task<TDocument> FindByIdAsync(string id);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task InsertOneAsync(TDocument document);
        Task UpdateOneAsync(TDocument document);
    }
}