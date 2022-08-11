using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.ReportModels
{
    public class ReportModel
    {
        public List<IncomeCategoryReport>? IncomeReports { get; set; }
        public List<ExpenseCategoryReport>? ExpenseReports { get; set; }
        public double? TotalIncome { get; set; }
        public double? TotalExpense { get; set; }
    }
}
