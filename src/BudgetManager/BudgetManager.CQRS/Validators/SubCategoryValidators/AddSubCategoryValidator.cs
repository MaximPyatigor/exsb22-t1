using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Validators
{
    public class AddSubCategoryValidator : AbstractValidator<AddSubCategoryDTO>
    {
        private readonly IMediator _mediator;
        private IEnumerable<CategoryResponse> _categories;

        public AddSubCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x.Name).NotEmpty()
                .Must(IsNameUnique).WithMessage($"Subcategory with this 'Name' already exists")
                .MaximumLength(100);
            RuleFor(x => x).Must(IsCategoryTypeExpense);

            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.CategoryType).Equal(OperationType.Expense);
        }

        public async Task SetUserAndCategory(Guid userId, Guid categoryId, CancellationToken cancellationToken)
        {
            _categories = await _mediator.Send(new GetCategoriesQuery(userId), cancellationToken);
        }

        public bool IsNameUnique(AddSubCategoryDTO category, string newValue)
        {
            return _categories.All(ca =>
              ca.Equals(category) || ca.Name.ToLower() != newValue.ToLower());
        }

        public bool IsCategoryTypeExpense(AddSubCategoryDTO category)
        {
            return _categories.Where(c => c.Id == category.CategoryId).Select(c => c.CategoryType).Equals(OperationType.Expense);
        }
    }
}
