using BudgetManager.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Queries.CurrencyRatesQueries
{
    public record ConvertCurrencyRatesQuery(string ConvertFrom, string ConvertTo, decimal Amount) : IRequest<decimal>;
}
