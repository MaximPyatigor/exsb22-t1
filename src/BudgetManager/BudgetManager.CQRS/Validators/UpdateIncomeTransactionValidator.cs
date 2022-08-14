using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;

namespace BudgetManager.CQRS.Validators
{
    public class UpdateIncomeTransactionValidator : AbstractValidator<UpdateIncomeTransactionDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private readonly ITransactionRepository _transactionRepository;
        private User _user;
        private Transaction _transaction;
        public UpdateIncomeTransactionValidator(IBaseRepository<User> repository, ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
            RuleFor(x => x.Id).Must(x => _transaction is not null)
                .WithMessage("Transaction does not exist");
            RuleFor(x => x.WalletId).NotEmpty().Must(walletId => _user.Wallets.Any(x => x.Id == walletId))
                 .WithMessage("Wallet does not exist");
            RuleFor(x => x.CategoryId).NotEmpty().Must(HasCategory)
                .WithMessage("Category does not exist");
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(200);
        }

        public async Task SetUserAsync(Guid userId, Guid transactionId, CancellationToken cancellationToken)
        {
            _user = await _repository.FindByIdAsync(userId, cancellationToken);
            _transaction = await _transactionRepository.FindByIdAsync(transactionId, cancellationToken);
        }

        private bool HasCategory(UpdateIncomeTransactionDTO transaction, Guid categoryId)
        {
            var categories = _user.Categories.Where(x => x.CategoryType == Model.Enums.OperationType.Income);
            return categories.Any(x => x.Id == categoryId);
        }
    }
}
