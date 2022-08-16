using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("RefreshTokens")]
    public class RefreshToken : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Expires { get; set; } = DateTime.Now.AddDays(3);
    }
}
