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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOneCategoryQuery(id), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertOne(AddCategoryDTO category, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddCategoryCommand(category), cancellationToken);
            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateOne(UpdateCategoryDTO category, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UpdateCategoryCommand(category), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteOne(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return response ? Ok(response) : NotFound();
        }
    }
}
