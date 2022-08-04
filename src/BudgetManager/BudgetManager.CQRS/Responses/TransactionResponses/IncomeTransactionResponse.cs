using BudgetManager.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class IncomeTransactionResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public OperationType TransactionType { get; set; }

        public string Description { get; set; }
    }
}
