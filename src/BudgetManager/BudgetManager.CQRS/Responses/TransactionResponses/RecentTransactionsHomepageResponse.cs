﻿using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class RecentTransactionsHomepageResponse
    {
        public Guid TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public string CategoryName { get; set; }

        public decimal Amount { get; set; }

        public Wallet Wallet { get; set; }
    }
}
