using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Validators
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private IEnumerable<Category> categories;

        public AddCategoryValidator(IBaseRepository<User> repository)
        {
            RuleFor(x => x.Name).NotEmpty()
                .Must(IsNameUnique).WithMessage($"Category with this 'Name' already exists")
                .MaximumLength(100);

            RuleFor(x => x.CategoryType).NotNull().IsInEnum();

            RuleFor(x => x.Color).NotEmpty();

            _repository = repository;
        }

        public void SetUser(Guid userId, OperationType categoryType, CancellationToken cancellationToken)
        {
            categories = _repository.FindByIdAsync(userId, cancellationToken).Result.Categories.Where(x=>x.CategoryType == categoryType);
        }

        public bool IsNameUnique(AddCategoryDTO category, string newValue)
        {
            if (!categories.Any())
            {
                return true;
            }

            return categories.All(ca =>
              ca.Equals(category) || ca.Name.ToLower() != newValue.ToLower());
        }
    }
}
