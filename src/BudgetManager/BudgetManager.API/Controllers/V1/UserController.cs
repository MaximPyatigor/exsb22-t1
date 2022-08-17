using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var user = await _mediator.Send(new GetUserByIdQuery(userId), cancellationToken);

            return user is not null ? Ok(user) : NotFound();
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            return await _mediator.Send(new DeleteUserCommand(userId), cancellationToken) ? Ok() : NotFound();
        }

        [HttpGet("Payers")]
        public async Task<IActionResult> GetUserPayersAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new GetUserPayersQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost("Payers")]
        public async Task<IActionResult> AddUserPayerAsync(string payerName, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new AddUserPayerCommand(userId, payerName), cancellationToken);

            return result is not null ? Ok() : BadRequest();
        }

        [HttpGet("TotalBalance")]
        public async Task<IActionResult> GetTotalBalanceAsync(string currencyCode, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new CalculateUserTotalBalanceQuery(userId, currencyCode), cancellationToken);
            return Ok(result);
        }
    }
}
