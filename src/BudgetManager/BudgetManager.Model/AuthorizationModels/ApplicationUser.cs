using AspNetCore.Identity.MongoDbCore.Models;

namespace BudgetManager.Model.AuthorizationModels
{
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public Guid UserId { get; set; }
    }
}
