using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;

        public DeleteCategoryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var userId = request.deleteDto.UserId;
            var categoryId = request.deleteDto.Id;
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user == null) { return false; }

            var category = user.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (category == null) { return false; }

            var result = user.Categories.Remove(category);
            if (result)
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
                var update = Builders<User>.Update.Set(u => u.Categories, user.Categories);

                await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
            } else
            {
                return false;
            }

            return result;
        }
    }
}
