using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Queries.ReportQueries;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model.Enums;
using BudgetManager.Model.ReportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.ReportHandlers
{
    public class GetReportHandler : IRequestHandler<GetReportQuery, Report>
    {
        private readonly IMediator _mediator;

        public GetReportHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Report> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            Report report = new Report();
            var incomeTransactions = (await _mediator
                .Send(new GetIncomeTransactionListByReportRequestQuery(request.UserId, request.ReportRequestInfo), cancellationToken))
                .ToList();

            // For each requested income categoryId, sum transaction value.
            decimal incomeCategoryTotal;
            decimal incomeCategoriesGrandTotal = 0;
            foreach (var incomeCategoryId in request.ReportRequestInfo.IncomeCategoryIds)
            {
                var category = await _mediator.Send(new GetOneCategoryQuery(request.UserId, incomeCategoryId), cancellationToken);
                // Check if null and category type incase expense category is passed.
                if (category == null) { continue; }
                if (category.CategoryType == OperationType.Expense) { continue; }

                incomeCategoryTotal = incomeTransactions.Where(t => t.CategoryId == incomeCategoryId).Sum(t => t.Value);

                var incomeReport = new IncomeCategoryReport()
                {
                    CategoryName = category.Name,
                    TransactionSum = Math.Round(incomeCategoryTotal, 2),
                };

                report.IncomeReports.Add(incomeReport);
                incomeCategoriesGrandTotal += incomeCategoryTotal;
            }
            report.TotalIncome = incomeCategoriesGrandTotal;

            return report;
        }
    }
}
