using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Shared.DataAccess.MongoDB
{
    public class ExadelBudgetDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CategoryCollectionName { get; set; } = null!;
        public string NotificationCollectionName { get; set; } = null!;
        public string TransactionCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string WalletCollectionName { get; set; } = null!;
    }
}
