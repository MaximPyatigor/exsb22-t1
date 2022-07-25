using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.SDK.DTO.CategoryDTOs;
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

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertOne(AddCategoryDTO category)
        {
            var response = await _mediator.Send(new AddCategoryCommand(category));
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetCategoriesQuery());
            return Ok(response);
        }
    }
}
