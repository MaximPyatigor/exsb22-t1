using System.Linq.Expressions;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        private readonly IMongoCollection<TDocument> _collection;
        private readonly IMongoClient _client;

        public BaseRepository(IMongoDbSettings settings, IMongoClient client)
        {
            _client = client;
            var database = _client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual Task InsertOneAsync(TDocument document, CancellationToken cancellationToken)
        {
            return Task.Run(() => _collection.InsertOneAsync(document, null, cancellationToken));
        }

        public virtual Task<TDocument> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
            });
        }

        public virtual async Task ReplaceOneAsync(TDocument document, CancellationToken cancellationToken)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document, null, cancellationToken);
        }

        public virtual Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter, null, cancellationToken);
            });
        }

        public virtual Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression, null, cancellationToken));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((CollectionNameAttribute)documentType.GetCustomAttributes(typeof(CollectionNameAttribute), true)
                .FirstOrDefault())?.Name;
        }
    }
}
