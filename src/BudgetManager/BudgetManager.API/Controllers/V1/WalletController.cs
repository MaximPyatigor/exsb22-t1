using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
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
        private const string _userIdString = "UserId";
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateWalletAsync([FromBody] UpdateWalletDTO updateWallet, bool isDefault, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new UpdateWalletCommand(userId, isDefault, updateWallet), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetActiveWalletsAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new GetActiveWalletsListQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllWalletsAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new GetAllWalletsListQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpGet("{walletId}")]
        public async Task<IActionResult> GetWalletByIdAsync(Guid walletId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new GetWalletByIdQuery(userId, walletId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpGet("Info")]
        public async Task<IActionResult> GetWalletInfoAsync(Guid walletId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new GetWalletInfoQuery(userId, walletId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWalletAsync([FromBody] AddWalletDTO walletDTO, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new AddWalletCommand(userId, walletDTO), cancellationToken);

            return result == Guid.Empty ? BadRequest() : Ok(result);
        }

        [Authorize]
        [HttpDelete("{walletId}")]
        public async Task<IActionResult> DeleteUserWalletAsync(Guid walletId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new DeleteUserWalletCommand(userId, walletId), cancellationToken);

            return result ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpGet("RecentTransactions")]
        public async Task<IActionResult> GetWalletRecentTransactionsAsync([FromQuery] WalletRecentTransactionsPageDTO recentTransactionsPageDTO, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new GetWalletRecentTransactionsQuery(userId, recentTransactionsPageDTO), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [Authorize]
        [HttpGet("Categories")]
        public async Task<IActionResult> GetWalletCategoriesAsync([FromQuery] WalletCategoriesDTO walletCategoriesDTO, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var result = await _mediator.Send(new GetWalletCategoriesListQuery(userId, walletCategoriesDTO), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }
    }
}
