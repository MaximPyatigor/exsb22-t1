using System.Linq.Expressions;
namespace BudgetManager.Shared.DataAccess.MongoDB.BaseRepository
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument>
        where TDocument : class
    {
        public BaseRepository()
        {

        }
        public Task InsertManyAsync(ICollection<TDocument> documents)
        {
            
        }

        public Task InsertOneAsync(TDocument document)
        {
            
        }

        public Task DeleteByIdAsync(string id)
        {
            
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            
        }

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            
        }

        public Task<TDocument> FindByIdAsync(string id)
        {
            
        }

        public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            
        }

        

        public Task UpdateOneAsync(TDocument document)
        {
            
        }
    }
}
