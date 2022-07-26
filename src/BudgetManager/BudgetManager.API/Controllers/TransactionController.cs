using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
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
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionDTO transaction, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddTransactionCommand(transaction), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionDTO updateTransaction, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateTransactionCommand(updateTransaction), cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteTransactionCommand(id), cancellationToken);
            return response == false ? NotFound() : Ok();
        }
    }
}
