using BudgetManager.CQRS.Responses.UserResponses;

namespace BudgetManager.Authorization
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public UserResponse User { get; set; }
    }
}
