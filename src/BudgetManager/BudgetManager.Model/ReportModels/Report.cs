using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.Model.ReportModels
{
    public class Report
    {
        public List<IncomeCategoryReport> IncomeReports { get; set; } = new List<IncomeCategoryReport>();
        public List<ExpenseCategoryReport> ExpenseReports { get; set; } = new List<ExpenseCategoryReport>();
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? TotalIncome { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? TotalExpense { get; set; }
    }
}
