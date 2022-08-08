namespace BudgetManager.CQRS.Responses.CurrencyResponses
{
    public class CurrencyResponse
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
    }
}
