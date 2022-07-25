using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model;
using BudgetManager.SDK;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetTransactionListQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetTransactionByIdQuery(id), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionDto transaction, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddTransactionCommand(transaction), cancellationToken);
            return response == null ? BadRequest() : Ok(response);
        }
    }
}
