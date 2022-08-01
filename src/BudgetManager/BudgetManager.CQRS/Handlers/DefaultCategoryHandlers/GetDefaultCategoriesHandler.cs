using AutoMapper;
using BudgetManager.CQRS.Queries.DefaultCategoryQueries;
using BudgetManager.CQRS.Responses.DefaultCategoryResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.DefaultCategoryHandlers
{
    public class GetDefaultCategoriesHandler : IRequestHandler<GetDefaultCategoriesQuery, IEnumerable<DefaultCategoryResponse>>
    {
        private readonly IDefaultCategory _defaultCategoryRepository;
        private readonly IMapper _mapper;

        public GetDefaultCategoriesHandler(IDefaultCategory baseRepository,
            IMapper mapper)
        {
            _defaultCategoryRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DefaultCategoryResponse>> Handle(GetDefaultCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allDefaultCategories = await _defaultCategoryRepository.GetAllAsync(cancellationToken);
            var mappedDefaultCategories = _mapper.Map<IEnumerable<DefaultCategoryResponse>>(allDefaultCategories);

            return mappedDefaultCategories;
        }
    }
}
