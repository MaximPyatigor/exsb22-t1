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
        private const string _userIdString = "UserId";
        private readonly IMediator _mediator;
        private readonly AddSubCategoryValidator _addValidator;
        private readonly UpdateCategoryValidator _updateValidator;
        public CategoryController(IMediator mediator, AddSubCategoryValidator addValidator, UpdateCategoryValidator updateValidator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _addValidator = addValidator ?? throw new ArgumentNullException(nameof(addValidator));
            _updateValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetCategoriesQuery(userId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new GetOneCategoryQuery(userId, categoryId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOneAsync([FromBody] AddCategoryDTO category, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            _addValidator.SetUser(userId, category.CategoryType, cancellationToken);
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
        public async Task<IActionResult> UpdateOneAsync(Guid categoryId, [FromBody] UpdateCategoryDTO category, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            await _updateValidator.SetUserAsync(userId, categoryId, cancellationToken);
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
        public async Task<IActionResult> DeleteOneAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(_userIdString).Value);
            var response = await _mediator.Send(new DeleteCategoryCommand(userId, categoryId), cancellationToken);
            return response ? Ok(response) : NotFound();
        }
    }
}
