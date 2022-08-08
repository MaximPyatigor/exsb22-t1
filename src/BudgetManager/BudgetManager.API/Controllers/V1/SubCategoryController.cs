﻿using BudgetManager.CQRS.Commands.SubCategoryCommands;
using BudgetManager.SDK.DTOs;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SubCategoryController : Controller
    {
        private readonly IMediator _mediator;
        public SubCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertOne(AddSubCategoryDTO category, CancellationToken cancellationToken)
        {
            Guid userId = new Guid(User.FindFirst("UserId").Value);
            var response = await _mediator.Send(new AddSubCategoryCommand(category, userId), cancellationToken);
            return response == Guid.Empty ? BadRequest() : Ok(response);
        }
    }
}
