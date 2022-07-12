using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{    
    public class Category
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Limit { get; set; }
        [Required]
        public LimitPeriods LimitPeriod { get; set; }
        [Required]
        public List<Guid> SubCategories { get; set; }
        [Required]
        public CategoryTypes CategoryType { get; set; }
        [Required]
        public string Color { get; set; }
    }
}