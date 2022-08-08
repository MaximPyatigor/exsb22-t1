using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.SDK.DTOs;
using MediatR;
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

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetUsersQuery(), cancellationToken);

            return users is not null ? Ok(users) : NotFound();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);

            return user is not null ? Ok(user) : NotFound();
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddNewUser([FromBody] AddUserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new AddUserCommand(userDTO), cancellationToken);

            return user == Guid.Empty ? BadRequest() : Ok(user);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUser, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateUserCommand(updateUser), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new DeleteUserCommand(id), cancellationToken) ? Ok() : NotFound();
        }

        [HttpGet("Payers")]
        public async Task<IActionResult> GetUserPayers(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserPayersQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost("Payers")]
        public async Task<IActionResult> AddUserPayer(Guid userId, string payerName, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserPayerCommand(userId, payerName), cancellationToken);

            return result is not null ? Ok() : BadRequest();
        }
    }
}
