using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Validators;
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
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly AddCategoryValidator _addValidator;
        private readonly UpdateCategoryValidator _updateValidator;
        private string _userIdString = "UserId";
        public CategoryController(IMediator mediator, AddCategoryValidator addValidator, UpdateCategoryValidator updateValidator)
        {
            _mediator = mediator;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetCategoriesQuery(userId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(Guid categoryId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetOneCategoryQuery(userId, categoryId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOne([FromBody] AddCategoryDTO category, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            _addValidator.SetUser(userId, cancellationToken);
            var validationResult = await _addValidator.ValidateAsync(category);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new AddCategoryCommand(userId, category), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateOne(Guid categoryId, [FromBody] UpdateCategoryDTO category, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            _updateValidator.SetUser(userId, cancellationToken);
            var validationResult = await _updateValidator.ValidateAsync(category);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new UpdateCategoryCommand(userId, categoryId, category), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteOne(Guid categoryId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new DeleteCategoryCommand(userId, categoryId), cancellationToken);
            return response ? Ok(response) : NotFound();
        }
    }
}
