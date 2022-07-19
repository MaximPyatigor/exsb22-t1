using System.Linq.Expressions;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument>
        where TDocument : IModelBase
    {
        private readonly IMongoCollection<TDocument> _collection;
        public BaseRepository(IMongoDbSettings settings)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
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

        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual Task<TDocument> FindByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public virtual Task DeleteByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public virtual Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((CollectionNameAttribute)documentType.GetCustomAttributes(typeof(CollectionNameAttribute), true)
                .FirstOrDefault())?.Name;
        }
    }
}
