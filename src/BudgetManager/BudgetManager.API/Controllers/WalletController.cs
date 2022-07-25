using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
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
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletDTO updateWallet)
        {
            var result = await _mediator.Send(new UpdateWalletCommand(updateWallet));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetWallets()
        {
            var result = await _mediator.Send(new GetWalletListQuery());

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetWalletById(Guid id)
        {
            var result = await _mediator.Send(new GetWalletByIdQuery(id));

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] AddWalletDTO walletDTO)
        {
            var result = await _mediator.Send(new AddWalletCommand(walletDTO));

            return result == Guid.Empty ? BadRequest() : Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var result = await _mediator.Send(new DeleteWalletCommand(id));

            return result ? Ok() : NotFound();
        }
    }
}
