using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;

namespace BudgetManager.CQRS.Validators
{
    public class AddPiggyBankValidator : AbstractValidator<AddPiggyBankDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private IEnumerable<PiggyBank> _piggyBanks;

        public AddPiggyBankValidator(IBaseRepository<User> repository)
        {
            _repository = repository;
            RuleFor(x => x.Name).NotEmpty()
                .Must(IsNameUnique).WithMessage($"Piggy Bank with this 'Name' already exists")
                .MaximumLength(30);
            RuleFor(x => x.CurrencyId).NotEmpty();
        }

        public async Task SetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            _piggyBanks =  _repository.FindByIdAsync(userId, cancellationToken).Result.PiggyBanks;
        }

        public bool IsNameUnique(AddPiggyBankDTO addPiggy, string newValue)
        {
            if (!_piggyBanks.Any())
            {
                return true;
            }

            return _piggyBanks.All(ca =>
              ca.Equals(addPiggy) || ca.Name.ToLower() != newValue.ToLower());
        }
    }
}
