using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Model.ReportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTransactionListByReportRequestQuery(Guid UserId, ReportRequest ReportRequestInfo, OperationType TransactionType) : IRequest<IEnumerable<Transaction>>;
}
