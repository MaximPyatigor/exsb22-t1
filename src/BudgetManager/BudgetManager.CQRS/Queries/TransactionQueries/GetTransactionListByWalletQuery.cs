using BudgetManager.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTransactionListByWalletQuery(Guid WalletId) : IRequest<IEnumerable<Transaction>>;
}
