using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.CQRS.Queries.CountryQueries;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private string _userIdString = "UserId";
        public CountryController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllCoutries(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCountryListQuery(), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(UpdateCountryDTO updateCountryDTO, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new UpdateCountryCommand(userId, updateCountryDTO), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
