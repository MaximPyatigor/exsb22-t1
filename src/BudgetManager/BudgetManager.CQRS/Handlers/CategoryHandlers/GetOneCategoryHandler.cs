using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

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

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, c => c.Id == categoryId);
            var projection = Builders<User>.Projection.Include(u => u.Categories)
                .ElemMatch(u => u.Categories, c => c.Id == categoryId);

            var response = await _userRepository.FilterBy<User>(filter, projection, cancellationToken);
            var user = response.FirstOrDefault();

            if (user == null ) { return null; }

            var usersCategory = user.Categories.FirstOrDefault();
            var mappedCategory = _mapper.Map<CategoryResponse>(usersCategory);
            return mappedCategory;
        }
    }
}
