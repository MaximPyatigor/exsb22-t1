﻿using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class ExpenseTransactionResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

        public string Payer { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public OperationType TransactionType { get; set; }

        public string Description { get; set; }
    }
}
