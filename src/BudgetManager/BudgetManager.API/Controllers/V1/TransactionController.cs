using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model.Enums;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetTransactionByIdQuery(id), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [Authorize]
        [HttpGet("Expense")]
        public async Task<IActionResult> GetExpenseTransactionList(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetExpenseTransactionListQuery(userId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [Authorize]
        [HttpGet("Income")]
        public async Task<IActionResult> GetIncomeTransactionList(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetIncomeTransactionListQuery(userId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [Authorize]
        [HttpPost("Expense")]
        public async Task<IActionResult> AddExpenseTransaction([FromBody] AddExpenseTransactionDTO expenseTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new AddExpenseTransactionCommand(userId, expenseTransaction), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [Authorize]
        [HttpPost("Income")]
        public async Task<IActionResult> AddIncomeTransaction([FromBody] AddIncomeTransactionDTO incomeTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new AddIncomeTransactionCommand(userId, incomeTransaction), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [Authorize]
        [HttpPut("Income")]
        public async Task<IActionResult> UpdateIncomeTransaction([FromBody] UpdateIncomeTransactionDTO incomeTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new UpdateIncomeTransactionCommand(userId, incomeTransaction), cancellationToken);
            return response is not null ? Ok(response) : NotFound();
        }

        [Authorize]
        [HttpDelete("Expense")]
        public async Task<IActionResult> DeleteExpenseTransaction(Guid expenseId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new DeleteExpenseTransactionCommand(userId, expenseId), cancellationToken);

            return result ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpDelete("Income")]
        public async Task<IActionResult> DeleteIncome(Guid incomeId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new DeleteIncomeTransactionCommand(userId, incomeId), cancellationToken);

            return result ? Ok() : BadRequest();
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
