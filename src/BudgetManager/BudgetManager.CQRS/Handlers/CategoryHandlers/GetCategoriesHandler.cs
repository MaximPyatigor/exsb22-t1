using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var projection = Builders<User>.Projection.Include(u => u.Categories);
            var response = await _userRepository.FilterBy<User>(filter, projection, cancellationToken);
            var listOfUsers = response.ToList();

            if (response is null || listOfUsers is null || listOfUsers.Count < 1) { return null; }

            var usersCategories = listOfUsers[0].Categories;
            var listOfResponseCategories = _mapper.Map<IEnumerable<CategoryResponse>>(usersCategories);

            return listOfResponseCategories;
        }
    }
}
