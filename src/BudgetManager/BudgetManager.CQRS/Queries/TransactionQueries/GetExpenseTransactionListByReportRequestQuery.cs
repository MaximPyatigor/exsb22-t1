using BudgetManager.Model;
using BudgetManager.Model.ReportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetExpenseTransactionListByReportRequestQuery(Guid UserId, ReportRequest ReportRequestInfo) : IRequest<IEnumerable<Transaction>>;
}
