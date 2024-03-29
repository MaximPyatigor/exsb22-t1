﻿using AutoMapper;
using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CountryHandlers
{
    public class AddManyCountriesHandler : IRequestHandler<AddManyCountriesCommand, IEnumerable<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Country> _dataAccess;

        public AddManyCountriesHandler(IMapper mapper, IBaseRepository<Country> dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<IEnumerable<Guid>> Handle(AddManyCountriesCommand request, CancellationToken cancellationToken)
        {
            var counries = request.countries;
            await _dataAccess.InsertManyAsync(counries, cancellationToken);

            return counries.Select(c => c.Id);
        }
    }
}
