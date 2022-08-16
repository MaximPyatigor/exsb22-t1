using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;

namespace BudgetManager.CQRS.Validators
{
    public class UpdateExpenseTransactionValidator : AbstractValidator<UpdateExpenseTransactionDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private readonly ITransactionRepository _transactionRepository;
        private User _user;
        private Transaction _transaction;

        public UpdateExpenseTransactionValidator(IBaseRepository<User> repository, ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
            RuleFor(x => x.Id).Must(x => _transaction is not null)
                 .WithMessage("Transaction does not exist");
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

        public async Task SetUserAsync(Guid userId, Guid transactionId, CancellationToken cancellationToken)
        {
            _user = await _repository.FindByIdAsync(userId, cancellationToken);
            _transaction = await _transactionRepository.FindByIdAsync(transactionId, cancellationToken);
        }

        private bool HasCategory(UpdateExpenseTransactionDTO transaction, Guid categoryId)
        {
            var categories = _user.Categories.Where(x => x.CategoryType == Model.Enums.OperationType.Expense);
            return categories.Any(x => x.Id == categoryId);
        }

        private bool HasSubCategory(UpdateExpenseTransactionDTO transaction, Guid subCategoryId)
        {
            if (subCategoryId == Guid.Empty)
            {
                return true;
            }

            var categories = _user.Categories.Where(x => x.CategoryType == Model.Enums.OperationType.Expense);
            var category = categories.SingleOrDefault(x => x.Id == transaction.CategoryId);
            return category.SubCategories.Any(x => x.Id == subCategoryId);
        }
    }
}
