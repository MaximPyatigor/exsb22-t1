using BudgetManager.CQRS.Queries.ReportQueries;
using BudgetManager.CQRS.Validators;
using BudgetManager.Model.ReportModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private const string _userIdString = "UserId";
        private readonly IMediator _mediator;
        private readonly GetReportValidator _getReportValidator;

        public ReportController(IMediator mediator, GetReportValidator getReportValidator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _getReportValidator = getReportValidator ?? throw new ArgumentNullException(nameof(getReportValidator));
        }

        [HttpGet]
        public async Task<IActionResult> GetReportAsync([FromQuery] ReportRequest reportRequest, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);

            _getReportValidator.SetUser(userId, cancellationToken);
            var validationResult = await _getReportValidator.ValidateAsync(reportRequest);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new GetReportQuery(userId, reportRequest), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
