using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allCategoriesFromDb = await _categoryRepository.GetAllAsync(cancellationToken);
            var listOfResponseCategories = _mapper.Map<IEnumerable<CategoryResponse>>(allCategoriesFromDb);

            return listOfResponseCategories;
        }
    }
}
