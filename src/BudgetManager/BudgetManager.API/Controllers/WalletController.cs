using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletDTO updateWallet, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateWalletCommand(updateWallet), cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetWallets(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetWalletListQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetWalletById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetWalletByIdQuery(id), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] AddWalletDTO walletDTO, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddWalletCommand(walletDTO), cancellationToken);

            return result == Guid.Empty ? BadRequest() : Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteWallet(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteWalletCommand(id), cancellationToken);

            return result ? Ok() : NotFound();
        }
    }
}
