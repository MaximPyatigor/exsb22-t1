using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.ReportModels
{
    public class ReportRequest
    {
        [Required]
        public Guid WalletId { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public List<Guid> IncomeCategoryIds { get; set; }
        public List<Guid> ExpenseCategoryIds { get; set; }
        public List<string> Payers { get; set; }
    }
}
