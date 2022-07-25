using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public DeleteCategoryHandler(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var idToDelete = request.id;
            var existingCategoryObject = await _categoryRepository.FindByIdAsync(idToDelete, cancellationToken);
            if (existingCategoryObject == null)
            {
                return false;
            }

            await _categoryRepository.DeleteByIdAsync(idToDelete, cancellationToken);
            return true;
        }
    }
}
