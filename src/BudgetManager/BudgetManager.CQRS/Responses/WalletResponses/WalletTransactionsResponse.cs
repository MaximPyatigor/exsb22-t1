using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.WalletResponses
{
    public class WalletTransactionsResponse
    {
        public Wallet Wallet { get; set; }

        public bool IsDefaultWallet { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
