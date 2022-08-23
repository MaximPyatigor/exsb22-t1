using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public DeleteCategoryHandler(IBaseRepository<User> userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {

            var userId = request.userId;
            var categoryId = request.categoryId;

            var categoryFilter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);
            var userFilter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, categoryFilter);
            var transactionFilter = Builders<Transaction>.Filter.And(
                Builders<Transaction>.Filter.Eq(t => t.UserId, userId),
                Builders<Transaction>.Filter.Eq(t => t.CategoryId, categoryId));
            var transactions = await _transactionRepository.FilterByAsync(transactionFilter, cancellationToken);

            if (transactions.Any())
            {
                throw new AppException("Categories with transactions cannot be deleted");
            }

            var update = Builders<User>.Update.PullFilter(u => u.Categories, categoryFilter);
            var result = await _userRepository.UpdateOneAsync(userFilter, update, cancellationToken);

            return result is not null;
        }
    }
}
