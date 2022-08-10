using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUser, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new UpdateUserCommand(userId, updateUser), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            return await _mediator.Send(new DeleteUserCommand(userId), cancellationToken) ? Ok() : NotFound();
        }

        [Authorize]
        [HttpGet("Payers")]
        public async Task<IActionResult> GetUserPayers(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new GetUserPayersQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpPost("Payers")]
        public async Task<IActionResult> AddUserPayer(string payerName, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new AddUserPayerCommand(userId, payerName), cancellationToken);

            return result is not null ? Ok() : BadRequest();
        }
    }
}
