using BudgetManager.CQRS.Queries.CurrencyQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrencyListQuery(), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("UserCurrencies")]
        public async Task<IActionResult> GetAllCurrenciesWithDefaultOnTop(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new GetCurrencyListWithDefaultOnTopQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }
    }
}
