using AutoMapper;
using BudgetManager.CQRS.Commands.PiggyBankCommands;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.PiggyBankHandlers
{
    public class AddPiggyBankHandler : IRequestHandler<AddPiggyBankCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _dataAccess;
        private readonly IMediator _mediator;

        public AddPiggyBankHandler(IMapper mapper, IBaseRepository<User> dataAccess, IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Guid> Handle(AddPiggyBankCommand request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            var piggyBank = _mapper.Map<PiggyBank>(request.AddPiggyBankDTO);

            piggyBank.Currency = await _mediator.Send(new GetCurrencyByIdQuery(request.AddPiggyBankDTO.CurrencyId), cancellationToken);

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Push(u => u.PiggyBanks, piggyBank);

            var result = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return piggyBank.Id;
        }
    }
}
