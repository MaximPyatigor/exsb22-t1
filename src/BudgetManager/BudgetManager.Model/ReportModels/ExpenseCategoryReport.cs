using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.ReportModels
{
    public class ExpenseCategoryReport
    {
        public string CategoryName { get; set; }
        public double TransactionSum { get; set; }
        public string Payer { get; set; }
    }
}
