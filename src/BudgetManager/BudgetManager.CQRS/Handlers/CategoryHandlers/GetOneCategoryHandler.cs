using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class GetOneCategoryHandler : IRequestHandler<GetOneCategoryQuery, CategoryResponse>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetOneCategoryHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(GetOneCategoryQuery request, CancellationToken cancellationToken)
        {
            var userId = request.queryDto.UserId;
            var categoryId = request.queryDto.Id;

            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                return null;
            }

            var category = user.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (category is null) { return null; }

            var mappedCategory = _mapper.Map<CategoryResponse>(category);
            return mappedCategory;
        }
    }
}
