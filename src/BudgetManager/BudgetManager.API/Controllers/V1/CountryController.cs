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
        private const string _userIdString = "UserId";
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet]
        public async Task<IActionResult> GetAllCoutriesAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCountryListQuery(), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountryAsync(UpdateCountryDTO updateCountryDTO, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new UpdateCountryCommand(userId, updateCountryDTO), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
