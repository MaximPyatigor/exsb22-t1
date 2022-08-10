using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private string _userIdString = "UserId";

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletDTO updateWallet, bool isDefault, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new UpdateWalletCommand(userId, isDefault, updateWallet), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWallets(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new GetWalletListQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] AddWalletDTO walletDTO, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new AddWalletCommand(userId, walletDTO), cancellationToken);

            return result == Guid.Empty ? BadRequest() : Ok(result);
        }

        [Authorize]
        [HttpDelete("{userId}/{walletId}")]
        public async Task<IActionResult> DeleteUserWallet(Guid walletId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new DeleteUserWalletCommand(userId, walletId), cancellationToken);

            return result ? Ok() : BadRequest();
        }
    }
}
