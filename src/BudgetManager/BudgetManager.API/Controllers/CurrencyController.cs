using BudgetManager.CQRS.Queries.CurrencyQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}