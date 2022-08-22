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
        public DateTimeOffset DateFrom { get; set; }
        [Required]
        public DateTimeOffset DateTo { get; set; }
        public List<Guid> IncomeCategoryIds { get; set; } = new List<Guid>();
        public List<Guid> ExpenseCategoryIds { get; set; } = new List<Guid>();
        public List<string> Payers { get; set; } = new List<string>();
    }
}
