using AutoMapper;
using BudgetManager.CQRS.Commands.SubCategoryCommands;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.SubCategoryHandlers
{
    public class AddSubCategoryHandler : IRequestHandler<AddSubCategoryCommand, Guid>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddSubCategoryHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
        {
            Guid userId = request.userId;
            Guid categoryId = request.categoryObject.CategoryId;
            AddSubCategoryDTO requestCategory = request.categoryObject;
            Category mappedCategory = _mapper.Map<Category>(requestCategory);

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId) &
                Builders<User>.Filter.ElemMatch(u => u.Categories, c => c.Id == categoryId);
            var update = Builders<User>.Update.Push(u => u.Categories[-1].SubCategories, mappedCategory);

            var result = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);

            return result is not null ? mappedCategory.Id : Guid.Empty;
        }
    }
}
