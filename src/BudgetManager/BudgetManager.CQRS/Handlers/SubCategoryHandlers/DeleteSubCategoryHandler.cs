using BudgetManager.CQRS.Commands.SubCategoryCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.DataAccess.MongoDbAccess.Repositories;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.SubCategoryHandlers
{
    public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBaseRepository<User> _userRepository;

        public DeleteSubCategoryHandler(ITransactionRepository transactionRepository, IBaseRepository<User> userRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetListByUserIdAndSubCategoryIdAsync(request.userId, request.subCategoryId, cancellationToken);
            if (transactions.Count() > 0) { throw new AppException("SubCategories with transactions cannot be deleted"); }

            var subCategoryFilter = Builders<Category>.Filter.Eq(c => c.Id, request.subCategoryId);

            var categoryFilter = Builders<Category>.Filter.Eq(c => c.Id, request.categoryId)
                & Builders<Category>.Filter.ElemMatch(c => c.SubCategories, subCategoryFilter);

            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, categoryFilter);

            var update = Builders<User>.Update.PullFilter(u => u.Categories[-1].SubCategories, subCategoryFilter);

            var result = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
            if (result is null) { throw new KeyNotFoundException("SubCategory not found"); }

            return true;
        }
    }
}
