using AutoMapper;
using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateCategoryObject = request.updateCategoryObject;
            var userId = request.userId;

            var categoryFilter = Builders<Category>.Filter.Eq(u => u.Id, request.categoryId);
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, categoryFilter);

            var mappedCategory = _mapper.Map<Category>(updateCategoryObject);

            var update = Builders<User>.Update
                .Set(u => u.Categories[-1].Name, mappedCategory.Name)
                .Set(u => u.Categories[-1].CategoryType, mappedCategory.CategoryType)
                .Set(u => u.Categories[-1].Color, mappedCategory.Color)
                .Set(u => u.Categories[-1].LimitPeriod, mappedCategory.LimitPeriod)
                .Set(u => u.Categories[-1].Limit, mappedCategory.Limit);

            var result = _userRepository.UpdateOneAsync(filter, update, cancellationToken)
                .Result.Categories.FirstOrDefault(x => x.Id == request.categoryId);

            return result is null ? null : _mapper.Map<CategoryResponse>(result);
        }
    }
}
