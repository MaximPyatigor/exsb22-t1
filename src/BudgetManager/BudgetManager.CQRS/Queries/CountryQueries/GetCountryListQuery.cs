using BudgetManager.CQRS.Responses.CountryResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.CountryQueries
{
    public record GetCountryListQuery : IRequest<IEnumerable<CountryResponse>>;
}
