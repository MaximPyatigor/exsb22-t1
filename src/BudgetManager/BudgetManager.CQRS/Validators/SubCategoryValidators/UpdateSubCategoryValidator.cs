using BudgetManager.SDK.DTOs;
using FluentValidation;
using MediatR;
using BudgetManager.CQRS.Queries.SubCategoryQueries;
using BudgetManager.CQRS.Responses.SubCategoryResponses;

namespace BudgetManager.CQRS.Validators.SubCategoryValidators
{
    public class UpdateSubCategoryValidator : AbstractValidator<UpdateSubCategoryDTO>
    {
        private IEnumerable<SubCategoryResponse> _subCategories;
        private readonly IMediator _mediator;

        public UpdateSubCategoryValidator(IMediator mediator)
        {
            RuleFor(x => x.Name).NotEmpty()
                .Must(IsNameUnique).WithMessage($"SubCategory with this 'Name' already exists")
                .MaximumLength(100);

            RuleFor(x => x.Color).NotEmpty();

            _mediator = mediator;
        }

        public async Task SetUserAndCategory(Guid userId, Guid categoryId, CancellationToken cancellationToken)
        {
            _subCategories = await _mediator.Send(new GetSubCategoriesQuery(userId, categoryId), cancellationToken);
        }

        public bool IsNameUnique(UpdateSubCategoryDTO subCategory, string newValue)
        {
            return _subCategories.All(s => s.Id == subCategory.SubCategoryId || s.Name.ToLower() != newValue.ToLower());
        }
    }
}
