using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.SDK.DTOs;
using FluentValidation;
using MediatR;

namespace BudgetManager.CQRS.Validators.WalletValidators
{
    public class AddWalletValidator : AbstractValidator<AddWalletDTO>
    {
        private IEnumerable<WalletResponse> _wallets;
        private readonly IMediator _mediator;

        public AddWalletValidator(IMediator mediator)
        {
            RuleFor(w => w.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(w => w.CurrencyId)
                .NotEmpty()
                .Must(IsWalletUnique).WithMessage("Such wallet already exists, please choose another currency or change the name of wallet");

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            _wallets = await _mediator.Send(new GetActiveWalletsListQuery(userId), cancellationToken);
        }

        public bool IsWalletUnique(AddWalletDTO newWalletDTO, Guid newWalletCurrencyId)
        {
            return _wallets.All(
                w => (w.Name.ToLower() != newWalletDTO.Name.ToLower()) || (w.Currency.Id != newWalletCurrencyId)
            );
        }
    }
}
