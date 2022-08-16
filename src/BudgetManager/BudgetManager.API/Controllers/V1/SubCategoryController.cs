using BudgetManager.CQRS.Commands.SubCategoryCommands;
using BudgetManager.CQRS.Queries.SubCategoryQueries;
using BudgetManager.CQRS.Validators;
using BudgetManager.CQRS.Validators.SubCategoryValidators;
using BudgetManager.Model;
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
    public class SubCategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UpdateSubCategoryValidator _updateValidator;

        public SubCategoryController(IMediator mediator, UpdateSubCategoryValidator updateValidator)
        {
            _mediator = mediator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertOne(AddSubCategoryDTO category, CancellationToken cancellationToken)
        {
            Guid userId = new Guid(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new AddSubCategoryCommand(category, userId), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid categoryId, CancellationToken cancellationToken)
        {
            Guid userId = new Guid(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetSubCategoriesQuery(userId, categoryId), cancellationToken);
            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOne(UpdateSubCategoryDTO subCategory, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            await _updateValidator.SetUserAndCategory(userId, subCategory.CategoryId, cancellationToken);
            var validationResult = await _updateValidator.ValidateAsync(subCategory);
            if (!validationResult.IsValid)
            {
                var result = ValidatorService.GetErrorMessage(validationResult);
                return BadRequest(result);
            }

            var response = await _mediator.Send(new UpdateSubCategoryCommand(userId, subCategory), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOne(Guid categoryId, Guid subCategoryId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new DeleteSubCategoryCommand(userId, categoryId, subCategoryId), cancellationToken);
            return response ? Ok(response) : NotFound();
        }
    }
}
