﻿using BudgetManager.CQRS.Responses.CurrencyResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.CurrencyQueries
{
    public record GetCurrencyListQuery() : IRequest<IEnumerable<CurrencyResponse>>;
}
