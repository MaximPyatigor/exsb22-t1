using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.SDK;
using BudgetManager.SDK.DTOs;
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
        public async Task<IActionResult> GetNotifications(Guid userId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNotificationListQuery(userId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNotificationByIdQuery(userId, id), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(Guid userId, [FromBody] AddNotificationDto notification, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddNotificationCommand(userId, notification), cancellationToken);
            return response == Guid.Empty ? NotFound("UserId not found") : Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationReadStatus(Guid id, bool isRead, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UpdateNotificationReadStatusCommand(id, isRead), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteNotificationCommand(id), cancellationToken);
            return response ? Ok() : NotFound();
        }
    }
}