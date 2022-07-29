using AutoMapper;
using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddCategoryHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            Guid userId = request.category.UserId;
            AddCategoryDTO requestCategory = request.category;
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);

            if (user is null) { return Guid.Empty; }

            Category mappedCategory = _mapper.Map<Category>(requestCategory);
            user.Categories.Add(mappedCategory);

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Categories, user.Categories);

            await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
            return mappedCategory.Id;
        }
    }
}
