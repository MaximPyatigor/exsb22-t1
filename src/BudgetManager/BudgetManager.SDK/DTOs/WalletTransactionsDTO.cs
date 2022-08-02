using BudgetManager.Model;

namespace BudgetManager.SDK.DTOs
{
    public class WalletTransactionsDTO
    {
        public Wallet Wallet { get; set; }
        public bool IsDefaultWallet { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
