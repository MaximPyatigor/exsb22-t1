using BudgetManager.CQRS.Commands.SubCategoryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.SubCategoryHandlers
{
    public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;

        public DeleteSubCategoryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
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
