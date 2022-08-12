using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.Model;
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
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private List<Category> categories;
        private Category oldCategory;

        public UpdateCategoryValidator(IBaseRepository<User> repository)
        {
            RuleFor(x => x.Name).NotEmpty()
                .Must(IsNameUnique).WithMessage($"Category with this 'Name' already exists")
                .MaximumLength(100);

            RuleFor(x => x.Color).NotEmpty();

            _repository = repository;
        }

        public async Task SetUserAsync(Guid userId, Guid categoryId, CancellationToken cancellationToken)
        {
            categories = _repository.FindByIdAsync(userId, cancellationToken).Result.Categories;
            oldCategory = categories.FirstOrDefault(x => x.Id == categoryId);
            categories = categories.Where(x => x.CategoryType == oldCategory.CategoryType).ToList();
        }

        public bool IsNameUnique(UpdateCategoryDTO category, string newValue)
        {
            if (oldCategory.Name.ToLower() == newValue.ToLower())
                return true;
            return categories.All(ca =>
              ca.Equals(category) || ca.Name.ToLower() != newValue.ToLower());
        }
    }
}
