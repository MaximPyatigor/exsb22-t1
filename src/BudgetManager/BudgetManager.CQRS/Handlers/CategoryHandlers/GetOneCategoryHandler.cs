using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class GetOneCategoryHandler: IRequestHandler<GetOneCategoryQuery, CategoryResponse>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetOneCategoryHandler(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(GetOneCategoryQuery request, CancellationToken cancellationToken)
        {
            var id = request.id;

            var category = await _categoryRepository.FindByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return null;
            }

            var mappedCategory = _mapper.Map<CategoryResponse>(category);
            return mappedCategory;
        }
    }
}
