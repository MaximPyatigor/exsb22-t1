using AspNetCore.Identity.MongoDbCore.Models;

namespace Infrastructure.Models
{
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public Guid UserId { get; set; }
    }
}
