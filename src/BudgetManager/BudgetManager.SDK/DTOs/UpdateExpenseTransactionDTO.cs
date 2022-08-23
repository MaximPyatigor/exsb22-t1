using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.SDK.DTOs
{
    public class UpdateExpenseTransactionDTO
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

        public string Payer { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
