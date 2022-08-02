using AspNetCore.Identity.MongoDbCore.Models;
using System;

namespace AuthorizationModels.Models
{
    public class ApplicationUser: MongoIdentityUser<Guid>
    {
        public Guid UserId { get; set; }
    }
}
