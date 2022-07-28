namespace BudgetManager.CQRS.Responses.CountryResponses
{
    public class CountryResponse
    {
        public Guid Id { get; set; }

        public string CountryName { get; set; }

        public string CurrencyCode { get; set; }
    }
}
