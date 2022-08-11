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
        [Required]
        public DateOnly DateFrom { get; set; }
        [Required]
        public DateOnly DateTo { get; set; }
        [Required]
        public List<Guid> IncomeCategoryIds { get; set; }
        [Required]
        public List<Guid> ExpenseCategoryIds { get; set; }
        [Required]
        public List<string> Payers { get; set; }
    }
}
