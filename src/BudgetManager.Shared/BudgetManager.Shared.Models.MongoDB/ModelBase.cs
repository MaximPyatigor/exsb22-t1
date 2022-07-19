using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.Shared.Models.MongoDB
{
    public class ModelBase : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
