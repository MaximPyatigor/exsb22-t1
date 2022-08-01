using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;
using System.Diagnostics;

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
            var categoryFilter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, categoryFilter);

            var user = await _userRepository.FilterBy(filter, cancellationToken);
            if (user == null) { return false; }
            else
            {
                var update = Builders<User>.Update.PullFilter(u => u.Categories, categoryFilter);
                await _userRepository.UpdateOneAsync(filter, update, cancellationToken);

                return true;
            }
        }
    }
}
