using BudgetManager.CQRS.Responses.UserResponses;

namespace BudgetManager.Authorization
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public UserAuthorizationObject User { get; set; }
    }
}
