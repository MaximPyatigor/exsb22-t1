using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.CountryCommands
{
    public record AddManyCountriesCommand(IEnumerable<Country> countries) : IRequest<IEnumerable<Guid>>;
}
