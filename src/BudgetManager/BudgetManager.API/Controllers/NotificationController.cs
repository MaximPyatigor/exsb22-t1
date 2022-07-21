using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.SDK;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var response = await _mediator.Send(new GetNotificationListQuery());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            var response = await _mediator.Send(new GetNotificationByIdQuery(id));
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationDto notification)
        {
            var response = await _mediator.Send(new AddNotificationCommand(notification));
            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationReadStatus(Guid id, bool isRead)
        {
            var response = await _mediator.Send(new UpdateNotificationReadStatusCommand(id, isRead));
            return response == null ? BadRequest() : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var response = await _mediator.Send(new DeleteNotificationCommand(id));
            return new NoContentResult();
        }
    }
}
