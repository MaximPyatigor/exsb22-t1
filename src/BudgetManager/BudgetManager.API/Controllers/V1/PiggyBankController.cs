using BudgetManager.CQRS.Commands.PiggyBankCommands;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [Authorize]
    public class PiggyBankController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PiggyBankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PiggyBankController>
        [HttpGet]
        public IEnumerable<string> GetPiggyBanks()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<PiggyBankController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPiggyBankDTO addPiggy, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new AddPiggyBankCommand(userId, addPiggy), cancellationToken);

            return result == Guid.Empty ? BadRequest() : Ok(result);
        }
    }
}
