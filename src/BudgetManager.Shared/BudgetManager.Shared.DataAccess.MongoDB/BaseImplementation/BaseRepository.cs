using System.Linq.Expressions;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public abstract class BaseRepository<TDocument> : IBaseRepository<TDocument>
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

        public virtual async Task<IEnumerable<TDocument>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _collection.Find(_ => true).ToListAsync(cancellationToken);
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression,
        CancellationToken cancellationToken)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable(cancellationToken);
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken)
        {
            return _collection.Find(filterExpression).ToEnumerable(cancellationToken);
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(FilterDefinition<TDocument> filterDefenition, ProjectionDefinition<TDocument, TProjected> projectDefinition, CancellationToken cancellationToken)
        {
            return _collection.Find(filterDefenition).Project(projectDefinition).ToEnumerable(cancellationToken);
        }

        public IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterDefenition, CancellationToken cancellationToken)
        {
            return _collection.Find(filterDefenition).ToEnumerable(cancellationToken);
        }

        public virtual async Task InsertOneAsync(TDocument document, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(document, null, cancellationToken);
        }

        public virtual async Task InsertManyAsync(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
        {
            await _collection.InsertManyAsync(documents, null, cancellationToken);
        }

        public virtual async Task<TDocument> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return await _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TDocument> ReplaceOneAsync(TDocument document, CancellationToken cancellationToken)
        {
            var options = new FindOneAndReplaceOptions<TDocument> { ReturnDocument = ReturnDocument.After };
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return await _collection.FindOneAndReplaceAsync(filter, document, options, cancellationToken);
        }

        public virtual async Task<TDocument> UpdateOneAsync(Expression<Func<TDocument, bool>> filterExpression,
            UpdateDefinition<TDocument> updateDefinition, CancellationToken cancellationToken)
        {
            var options = new FindOneAndUpdateOptions<TDocument> { ReturnDocument = ReturnDocument.After };
            return await _collection.FindOneAndUpdateAsync(filterExpression, updateDefinition, options, cancellationToken);
        }

        public virtual async Task<TDocument> UpdateOneAsync(FilterDefinition<TDocument> filterExpression,
            UpdateDefinition<TDocument> updateDefinition, CancellationToken cancellationToken)
        {
            var options = new FindOneAndUpdateOptions<TDocument> { ReturnDocument = ReturnDocument.After };
            return await _collection.FindOneAndUpdateAsync(filterExpression, updateDefinition, options, cancellationToken);
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            var deleteResult = await _collection.FindOneAndDeleteAsync(filter, null, cancellationToken);

            return deleteResult is not null;
        }

        public virtual async Task<bool> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken)
        {
            var deleteResult = await _collection.FindOneAndDeleteAsync(filterExpression, null, cancellationToken);

            return deleteResult is not null;
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((CollectionNameAttribute)documentType.GetCustomAttributes(typeof(CollectionNameAttribute), true)
                .FirstOrDefault())?.Name;
        }
    }
}
