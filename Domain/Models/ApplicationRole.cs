using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
 
namespace Domain.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
 
    }
}