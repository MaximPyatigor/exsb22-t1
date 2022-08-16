using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;

namespace BudgetManager.CQRS.Validators
{
    public class AddExpenseTransactionValidator : AbstractValidator<AddExpenseTransactionDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private User _user;

        public AddExpenseTransactionValidator(IBaseRepository<User> repository)
        {
            _repository = repository;
            RuleFor(x => x.WalletId).NotEmpty().Must(walletId => _user.Wallets.Any(x => x.Id == walletId))
                 .WithMessage("Wallet does not exist");
            RuleFor(x => x.CategoryId).NotEmpty().Must(HasCategory)
                .WithMessage("Category does not exist");
            RuleFor(x => x.SubCategoryId).Must(HasSubCategory)
                .WithMessage("Subcategory does not exist");
            RuleFor(x => x.Payer).NotEmpty().Must(p => _user.Payers.Any(c => c == p))
                .WithMessage("Payer does not exist");
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(200);
        }

        public async Task SetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            _user = await _repository.FindByIdAsync(userId, cancellationToken);
        }

        private bool HasCategory(AddExpenseTransactionDTO transaction, Guid categoryId)
        {
            var categories = _user.Categories.Where(x => x.CategoryType == Model.Enums.OperationType.Expense);
            return categories.Any(x => x.Id == categoryId);
        }

        private bool HasSubCategory(AddExpenseTransactionDTO transaction, Guid subCategoryId)
        {
            if (subCategoryId == Guid.Empty)
            {
                return true;
            }

            var categories = _user.Categories.Where(x => x.CategoryType == Model.Enums.OperationType.Expense);
            var category =  categories.SingleOrDefault(x => x.Id == transaction.CategoryId);
            return category.SubCategories.Any(x => x.Id == subCategoryId);
        }
    }
}
