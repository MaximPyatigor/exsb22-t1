using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class GetCategoryListByOperationHandler : IRequestHandler<GetCategoriesByOperationQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryListByOperationHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesByOperationQuery request, CancellationToken cancellationToken)
        {
            var allCategoriesFromDb = await _categoryRepository.GetListByOperationAsync(request.userId, request.operationType, cancellationToken);
            var listOfResponseCategories = _mapper.Map<IEnumerable<CategoryResponse>>(allCategoriesFromDb);

            return listOfResponseCategories;
        }
    }
}
