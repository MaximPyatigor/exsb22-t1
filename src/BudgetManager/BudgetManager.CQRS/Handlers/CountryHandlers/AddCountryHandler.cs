using AutoMapper;
using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CountryHandlers
{
    public class AddCountryHandler : IRequestHandler<AddCountryCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Country> _dataAccess;

        public AddCountryHandler(IMapper mapper, IBaseRepository<Country> dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Unit> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            await _dataAccess.InsertOneAsync(request.country, cancellationToken);
            return Unit.Value;
        }
    }
}
