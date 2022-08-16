using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;

namespace BudgetManager.CQRS.Validators
{
    public class AddIncomeTransactionValidator : AbstractValidator<AddIncomeTransactionDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private User _user;

        public AddIncomeTransactionValidator(IBaseRepository<User> repository)
        {
            _repository = repository;
            RuleFor(x => x.WalletId).NotEmpty().Must(walletId => _user.Wallets.Any(x => x.Id == walletId))
                 .WithMessage("Wallet does not exist");
            RuleFor(x => x.CategoryId).NotEmpty().Must(HasCategory)
                .WithMessage("Category does not exist");
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(200);
        }

        public async Task SetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            _user = await _repository.FindByIdAsync(userId, cancellationToken);
        }

        private bool HasCategory(AddIncomeTransactionDTO transaction, Guid categoryId)
        {
            var categories = _user.Categories.Where(x => x.CategoryType == Model.Enums.OperationType.Income);
            return categories.Any(x => x.Id == categoryId);
        }
    }
}
