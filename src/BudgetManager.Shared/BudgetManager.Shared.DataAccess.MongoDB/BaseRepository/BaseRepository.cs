using System.Linq.Expressions;
using BudgetManager.Shared.Abstractions.Interfaces;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseRepository
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument>
        where TDocument : class
    {
        public IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public Task DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOneAsync(TDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
