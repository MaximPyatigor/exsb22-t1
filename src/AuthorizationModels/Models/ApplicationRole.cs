using AspNetCore.Identity.MongoDbCore.Models;
using System;

namespace AuthorizationModels.Models
{
    public class ApplicationRole: MongoIdentityRole<Guid>
    {
    }
}
