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
            var userId = request.updateCategoryObject.UserId;

            var categoryFilter = Builders<Category>.Filter.Eq(u => u.Id, updateCategoryObject.Id);
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, categoryFilter);

            var user = await _userRepository.FilterBy(filter, cancellationToken);

            if (user is null) { return null; }
            else
            {
                var mappedCategory = _mapper.Map<Category>(updateCategoryObject);

                var update = Builders<User>.Update.Set(u => u.Categories[-1], mappedCategory);

                await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
                return _mapper.Map<CategoryResponse>(mappedCategory);
            }
        }
    }
}
