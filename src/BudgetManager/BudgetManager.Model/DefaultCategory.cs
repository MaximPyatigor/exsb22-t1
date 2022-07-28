using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetManager.Model.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json.Converters;

namespace BudgetManager.Model
{
    [CollectionName("DefaultCategories")]
    public class DefaultCategory
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<DefaultCategory>? SubCategories { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationType CategoryType { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
