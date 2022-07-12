using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
 
namespace Infrastructure.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
 
    }
}