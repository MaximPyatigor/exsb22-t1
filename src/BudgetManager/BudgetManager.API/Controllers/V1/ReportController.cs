using BudgetManager.CQRS.Queries.ReportQueries;
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
        public ReportController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet]
        public async Task<IActionResult> GetReportAsync([FromQuery] ReportRequest reportRequest, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetReportQuery(userId, reportRequest), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
