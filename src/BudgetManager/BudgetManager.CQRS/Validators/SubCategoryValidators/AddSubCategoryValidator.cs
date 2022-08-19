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
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(100).WithMessage($"Maximum name length exceeded");
            RuleFor(x => x.Name).Must(IsSubCategoryNameUnique).WithMessage($"Subcategory with this 'Name' already exists");

            RuleFor(x => x.CategoryType).Equal(OperationType.Expense);

            RuleFor(x => x.CategoryId)
                .Must(DoesCategoryExist).WithMessage("Category with this 'CategoryId' does not exist")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(IsCategoryNameUnique).WithMessage($"Subcategory 'Name' can not match the category name");
                    RuleFor(x => x).Must(IsCategoryTypeExpense).WithMessage($"Category must be of expense category type");
                });
        }

        public async Task SetUserAndCategory(Guid userId, Guid categoryId, CancellationToken cancellationToken)
        {
            _categories = await _mediator.Send(new GetCategoriesQuery(userId), cancellationToken);
        }

        public bool DoesCategoryExist(AddSubCategoryDTO subCategory, Guid newValue)
        {
            var category = _categories.Where(c => c.Id == subCategory.CategoryId).FirstOrDefault();
            return category is not null;
        }

        public bool IsSubCategoryNameUnique(AddSubCategoryDTO category, string newValue)
        {
            var subCategories = _categories
                .Where(c => c.Id == category.CategoryId)
                .Select(c => c.SubCategories)
                .FirstOrDefault();

            if (subCategories == null) { return true; }

            return subCategories
                .All(
                    sub => sub.Id.Equals(category.CategoryId)
                        || sub.Name.ToLower() != newValue.ToLower()
                );

        }

        public bool IsCategoryNameUnique(AddSubCategoryDTO category, string newValue)
        {
            var isCategoryNameUnique = _categories
                .Where(c => c.Id == category.CategoryId)
                .FirstOrDefault()?
                .Name.ToLower() != newValue.ToLower();

            return isCategoryNameUnique;
        }

        public bool IsCategoryTypeExpense(AddSubCategoryDTO category)
        {
            return _categories
                .Where(c => c.Id == category.CategoryId)
                .Select(c => c.CategoryType)
                .FirstOrDefault()
                .Equals(OperationType.Expense);
        }
    }
}
