﻿using AutoMapper;
using BudgetManager.CQRS.Queries.DefaultCategoryQueries;
using BudgetManager.CQRS.Responses.DefaultCategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.DefaultCategoryHandlers
{
    public class GetDefaultCategoriesHandler : IRequestHandler<GetDefaultCategoriesQuery, IEnumerable<DefaultCategoryResponse>>
    {
        private readonly IBaseRepository<DefaultCategory> _defaultCategoryRepository;
        private readonly IMapper _mapper;

        public GetDefaultCategoriesHandler(IBaseRepository<DefaultCategory> baseRepository,
            IMapper mapper)
        {
            _defaultCategoryRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<DefaultCategoryResponse>> Handle(GetDefaultCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allDefaultCategories = await _defaultCategoryRepository.GetAllAsync(cancellationToken);
            var mappedDefaultCategories = _mapper.Map<IEnumerable<DefaultCategoryResponse>>(allDefaultCategories);

            return mappedDefaultCategories;
        }
    }
}
