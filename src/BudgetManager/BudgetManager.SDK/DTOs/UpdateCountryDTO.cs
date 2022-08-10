namespace BudgetManager.SDK.DTOs
{
    public class UpdateCountryDTO
    {
        public Guid CountryId { get; set; }

        public bool SetDefaultCurrency { get; set; }
    }
}
