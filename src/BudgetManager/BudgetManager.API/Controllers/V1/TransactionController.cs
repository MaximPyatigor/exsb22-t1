using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Validators;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AddExpenseTransactionValidator _addExpenseValidator;
        private readonly UpdateExpenseTransactionValidator _updateExpenseValidator;
        private readonly AddIncomeTransactionValidator _addIncomeValidator;
        private readonly UpdateIncomeTransactionValidator _updateIncomeValidator;

        public TransactionController(IMediator mediator,
            AddExpenseTransactionValidator addExpenseValidator,
            UpdateExpenseTransactionValidator updateExpenseValidator,
            AddIncomeTransactionValidator addIncomeValidator,
            UpdateIncomeTransactionValidator updateIncomeValidator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _addExpenseValidator = addExpenseValidator ?? throw new ArgumentNullException(nameof(addExpenseValidator));
            _updateExpenseValidator = updateExpenseValidator ?? throw new ArgumentNullException(nameof(updateExpenseValidator));
            _addIncomeValidator = addIncomeValidator ?? throw new ArgumentNullException(nameof(addIncomeValidator));
            _updateIncomeValidator = updateIncomeValidator ?? throw new ArgumentNullException(nameof(updateIncomeValidator));
        }

        [HttpGet("Homepage")]
        public async Task<IActionResult> GetTenRecentTransactionsAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetRecentTransactionsQuery(userId), cancellationToken);

            return response is not null ? Ok(response) : NotFound();
        }

        [HttpGet("Expense")]
        public async Task<IActionResult> GetExpenseTransactionListAsync([FromQuery] ExpensesPageDTO expensePageDto, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetExpenseTransactionListQuery(userId, expensePageDto), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("Income")]
        public async Task<IActionResult> GetIncomeTransactionListAsync([FromQuery] IncomesPageDTO incomesPageDto, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetIncomeTransactionListQuery(userId, incomesPageDto), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost("Expense")]
        public async Task<IActionResult> AddExpenseTransactionAsync([FromBody] AddExpenseTransactionDTO expenseTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            await _addExpenseValidator.SetUserAsync(userId, cancellationToken);
            var validationResult = await _addExpenseValidator.ValidateAsync(expenseTransaction);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new AddExpenseTransactionCommand(userId, expenseTransaction), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [HttpPost("Income")]
        public async Task<IActionResult> AddIncomeTransactionAsync([FromBody] AddIncomeTransactionDTO incomeTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            await _addIncomeValidator.SetUserAsync(userId, cancellationToken);
            var validationResult = await _addIncomeValidator.ValidateAsync(incomeTransaction);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new AddIncomeTransactionCommand(userId, incomeTransaction), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [HttpPut("Income")]
        public async Task<IActionResult> UpdateIncomeTransactionAsync([FromBody] UpdateIncomeTransactionDTO incomeTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            await _updateIncomeValidator.SetUserAsync(userId, incomeTransaction.Id, cancellationToken);
            var validationResult = await _updateIncomeValidator.ValidateAsync(incomeTransaction);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new UpdateIncomeTransactionCommand(userId, incomeTransaction), cancellationToken);
            return response is not null ? Ok(response) : NotFound();
        }

        [HttpPut("Expense")]
        public async Task<IActionResult> UpdateExpenseTransactionAsync([FromBody] UpdateExpenseTransactionDTO updateExpenseTransaction, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            await _updateExpenseValidator.SetUserAsync(userId, updateExpenseTransaction.Id, cancellationToken);
            var validationResult = await _updateExpenseValidator.ValidateAsync(updateExpenseTransaction);
            if (!validationResult.IsValid)
            {
                var errors = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(errors);
            }

            var result = await _mediator.Send(new UpdateExpenseTransactionCommand(userId, updateExpenseTransaction), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpDelete("Expense")]
        public async Task<IActionResult> DeleteExpenseTransactionAsync(Guid expenseId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new DeleteExpenseTransactionCommand(userId, expenseId), cancellationToken);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("Income")]
        public async Task<IActionResult> DeleteIncomeAsync(Guid incomeId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new DeleteIncomeTransactionCommand(userId, incomeId), cancellationToken);

            return result ? Ok() : BadRequest();
        }
    }
}
