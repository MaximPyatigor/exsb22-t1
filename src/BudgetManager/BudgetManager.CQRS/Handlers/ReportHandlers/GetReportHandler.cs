using BudgetManager.CQRS.Queries.ReportQueries;
using BudgetManager.CQRS.Queries.TransactionQueries;
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
            var incomeTransactions = await _mediator
                .Send(new GetIncomeTransactionListByReportRequestQuery(request.UserId, request.ReportRequestInfo), cancellationToken);

            return new Report();
        }
    }
}
