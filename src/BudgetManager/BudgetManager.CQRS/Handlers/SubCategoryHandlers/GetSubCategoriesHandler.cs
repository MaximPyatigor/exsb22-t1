using AutoMapper;
using BudgetManager.CQRS.Queries.SubCategoryQueries;
using BudgetManager.CQRS.Responses.SubCategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.SubCategoryHandlers
{
    public class GetSubCategoriesHandler : IRequestHandler<GetSubCategoriesQuery, IEnumerable<SubCategoryResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetSubCategoriesHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubCategoryResponse>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, c => c.Id == request.categoryId);
            var projection = Builders<User>.Projection.Exclude(u => u.Id).Include(u => u.Categories[-1]);
            var response = await _userRepository.FilterByAsync<User>(filter, projection, cancellationToken);
            var categoryOfUser = response.FirstOrDefault().Categories;

            if (categoryOfUser == null) { throw new KeyNotFoundException("UserId or categoryId is not correct"); }

            var subCategoriesOfCategory = categoryOfUser.FirstOrDefault().SubCategories;
            var listOfResponseCategories = _mapper.Map<IEnumerable<SubCategoryResponse>>(subCategoriesOfCategory);

            return listOfResponseCategories;
        }
    }
}
