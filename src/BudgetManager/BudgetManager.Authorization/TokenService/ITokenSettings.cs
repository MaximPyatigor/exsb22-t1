namespace BudgetManager.Authorization.TokenService
{
    public interface ITokenSettings
    {
        public string JwtKey { get; set; }
    }
}
