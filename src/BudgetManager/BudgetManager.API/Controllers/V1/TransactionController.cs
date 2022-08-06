using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model.Enums;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetTransactions(Guid userId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetTransactionListQuery(userId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("{operationType:alpha}")]
        public async Task<IActionResult> GetTransactionsByOperation(Guid userId, OperationType operationType, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetTransactionListByOperationQuery(userId, operationType), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("{id:guid}")]
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

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteTransactionCommand(id), cancellationToken);
            return response ? Ok() : NotFound();
        }
    }
}
