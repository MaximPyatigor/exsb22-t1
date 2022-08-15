using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.CountryCommands
{
    public record AddCountryCommand(Country country) : IRequest<Unit>;
}
