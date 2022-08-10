using BudgetManager.CQRS.Responses.CountryResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.CountryCommands
{
    public record UpdateCountryCommand(Guid userId, UpdateCountryDTO updateCountryDTO) : IRequest<CountryResponse>;
}
