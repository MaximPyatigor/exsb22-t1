using BudgetManager.CQRS.Commands.PiggyBankCommands;
using BudgetManager.CQRS.Queries.PiggyBankQueries;
using BudgetManager.CQRS.Validators;
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
        private readonly AddPiggyBankValidator _validator;

        public PiggyBankController(IMediator mediator, AddPiggyBankValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        // GET: api/<PiggyBankController>
        [HttpGet]
        public async Task<IActionResult> GetPiggyBanks(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var result = await _mediator.Send(new GetPiggyBankListQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        // POST api/<PiggyBankController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPiggyBankDTO addPiggy, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            await _validator.SetUserAsync(userId, cancellationToken);
            var validationResult = await _validator.ValidateAsync(addPiggy, cancellationToken);
            if (!validationResult.IsValid)
            {
                var response = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(response);
            }

            var result = await _mediator.Send(new AddPiggyBankCommand(userId, addPiggy), cancellationToken);

            return result == Guid.Empty ? BadRequest() : Ok(result);
        }
    }
}
