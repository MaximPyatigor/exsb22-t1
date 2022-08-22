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
        private string _userId = "UserId";

        public PiggyBankController(IMediator mediator, AddPiggyBankValidator validator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        // GET: api/<PiggyBankController>
        [HttpGet]
        public async Task<IActionResult> GetPiggyBanksAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userId).Value);
            var result = await _mediator.Send(new GetPiggyBankListQuery(userId), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        // POST api/<PiggyBankController>
        [HttpPost]
        public async Task<IActionResult> CreatePiggyBankAsync([FromBody] AddPiggyBankDTO addPiggy, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userId).Value);
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
