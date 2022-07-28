using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.CQRS.Queries.CountryQueries;
using BudgetManager.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoutries(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCountryListQuery(), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }
    }
}
