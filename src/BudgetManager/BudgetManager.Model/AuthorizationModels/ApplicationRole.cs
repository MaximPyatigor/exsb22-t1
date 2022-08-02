using AspNetCore.Identity.MongoDbCore.Models;

namespace BudgetManager.Model.AuthorizationModels
{
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}
