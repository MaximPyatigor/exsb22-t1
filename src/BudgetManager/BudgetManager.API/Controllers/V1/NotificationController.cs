using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.SDK;
using BudgetManager.SDK.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private const string _userIdString = "UserId";
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetNotifications(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetNotificationListQuery(userId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationById(Guid notificationId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetNotificationByIdQuery(userId, notificationId), cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("NotifyAll")]
        public async Task<IActionResult> AddNotificationToAll([FromBody] AddNotificationDto notification, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddNotificationToAllCommand(notification), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationDto notification, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new AddNotificationCommand(userId, notification), cancellationToken);
            return Ok(response);
        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotificationReadStatus(Guid notificationId, bool isRead, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new UpdateNotificationReadStatusCommand(userId, notificationId, isRead), cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(Guid notificationId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            await _mediator.Send(new DeleteNotificationCommand(userId, notificationId), cancellationToken);
            return Ok();
        }
    }
}
