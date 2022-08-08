using BudgetManager.CQRS.Commands.DefaultCategoryCommands;
using BudgetManager.CQRS.Queries.DefaultCategoryQueries;
using BudgetManager.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DefaultCategoryController : Controller
    {
        private readonly IMediator _mediatr;
        public DefaultCategoryController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<DefaultCategory> defaultCategories,
            CancellationToken cancellationToken)
        {
            var response = await _mediatr.Send(new AddManyDefaultCategoriesCommand(defaultCategories), cancellationToken);
            return response == null ? BadRequest() : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediatr.Send(new GetDefaultCategoriesQuery(), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
