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
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);

            if (user is null) { return null; }

            var mappedCategory = _mapper.Map<Category>(updateCategoryObject);
            var userCategory = user.Categories.Where(c => c.Id == mappedCategory.Id).FirstOrDefault();
            var index = user.Categories.IndexOf(userCategory);

            if (userCategory is not null && index is not -1)
            {
                mappedCategory.Id = userCategory.Id;
                user.Categories[index] = mappedCategory;

                var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
                var update = Builders<User>.Update.Set(u => u.Categories, user.Categories);

                await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
                return _mapper.Map<CategoryResponse>(mappedCategory);
            } else
            {
                return null;
            }
        }
    }
}
