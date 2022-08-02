using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid userId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCategoriesQuery(userId), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOneCategoryQuery(userId, id), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOne(AddCategoryDTO category, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddCategoryCommand(category), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOne(UpdateCategoryDTO category, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UpdateCategoryCommand(category), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand(userId, id), cancellationToken);
            return response ? Ok(response) : NotFound();
        }
    }
}
