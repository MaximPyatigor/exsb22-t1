using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Validators;
using BudgetManager.CQRS.Validators.WalletValidators;
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
        private readonly AddWalletValidator _addWalletValidator;
        private readonly UpdateWalletValidator _updateWalletValidator;

        private readonly IMediator _mediator;
        private string _userIdString = "UserId";

        public WalletController(IMediator mediator, AddWalletValidator addWalletValidator, UpdateWalletValidator updateWalletValidator)
        {
            _mediator = mediator;
            _addWalletValidator = addWalletValidator;
            _updateWalletValidator = updateWalletValidator;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateWalletAsync([FromBody] UpdateWalletDTO updateWallet, bool isDefault, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);

            await _updateWalletValidator.SetUserAsync(userId, cancellationToken);
            var validationResult = await _updateWalletValidator.ValidateAsync(updateWallet);
            if (!validationResult.IsValid)
            {
                var validationErrorMessage = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(validationErrorMessage);
            }

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

            await _addWalletValidator.SetUserAsync(userId, cancellationToken);
            var validationResult = await _addWalletValidator.ValidateAsync(walletDTO);
            if (!validationResult.IsValid)
            {
                var validationErrorMessage = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(validationErrorMessage);
            }

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
