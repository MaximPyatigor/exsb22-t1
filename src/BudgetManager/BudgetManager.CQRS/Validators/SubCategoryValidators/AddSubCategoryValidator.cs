using BudgetManager.SDK.DTOs;
using FluentValidation;
using MediatR;
using BudgetManager.CQRS.Queries.SubCategoryQueries;
using BudgetManager.CQRS.Responses.SubCategoryResponses;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.CQRS.Queries.CategoryQueries;

namespace BudgetManager.CQRS.Validators.SubCategoryValidators
{
    public class AddSubCategoryValidator : AbstractValidator<AddSubCategoryDTO>
    {
        private CategoryResponse _category;
        private IEnumerable<SubCategoryResponse> _subCategories;
        private readonly IMediator _mediator;

        public AddSubCategoryValidator(IMediator mediator)
        {
            RuleFor(x => x.Name).NotEmpty()
                .Must(IsNameUnique).WithMessage($"SubCategory with this 'Name' already exists")
                .MaximumLength(100);

            RuleFor(x => x.Color).NotEmpty();

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SetUserAndCategory(Guid userId, Guid categoryId, CancellationToken cancellationToken)
        {
            _category = await _mediator.Send(new GetOneCategoryQuery(userId, categoryId), cancellationToken);
            _subCategories = await _mediator.Send(new GetSubCategoriesQuery(userId, categoryId), cancellationToken);
        }

        public bool IsNameUnique(AddSubCategoryDTO subCategory, string newValue)
        {
            return _subCategories.All(s => s.Name.ToLower() != newValue.ToLower());
        }
    }
}
