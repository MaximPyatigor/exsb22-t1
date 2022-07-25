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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletResponse>>> GetWallets()
        {
            var result = await _mediator.Send(new GetWalletListQuery());

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<WalletResponse>> GetWalletById(Guid id)
        {
            var result = await _mediator.Send(new GetWalletByIdQuery(id));

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateWallet(AddWalletDTO walletDTO)
        {
            var result = await _mediator.Send(new AddWalletCommand(walletDTO));

            return result == Guid.Empty ? BadRequest() : Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteWallet(Guid id)
        {
            var result = await _mediator.Send(new DeleteWalletCommand(id));

            return result ? Ok() : BadRequest();
        }
    }
}
