using BudgetManager.Model.ReportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Queries.ReportQueries
{
    public record GetReportQuery(ReportRequest InfoForReport) : IRequest<Report>;
}
