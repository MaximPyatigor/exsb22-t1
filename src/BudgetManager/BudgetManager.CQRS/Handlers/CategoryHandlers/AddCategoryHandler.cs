using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Unit>
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public AddCategoryHandler(IBaseRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public async Task<Unit> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            Category requestCategory = request.category;
            await this._categoryRepository.InsertOneAsync(requestCategory);
            return Unit.Value;
        }
    }
}
