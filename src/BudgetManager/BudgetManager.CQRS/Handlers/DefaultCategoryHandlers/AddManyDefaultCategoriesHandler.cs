using BudgetManager.CQRS.Commands.DefaultCategoryCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.DefaultCategoryHandlers
{
    public class AddManyDefaultCategoriesHandler : IRequestHandler<AddManyDefaultCategoriesCommand, IEnumerable<Guid>>
    {
        private readonly IDefaultCategory _defaultCategoryRepository;

        public AddManyDefaultCategoriesHandler(IDefaultCategory baseRepository)
        {
            _defaultCategoryRepository = baseRepository;
        }

        public async Task<IEnumerable<Guid>> Handle(AddManyDefaultCategoriesCommand request, CancellationToken cancellationToken)
        {
            await _defaultCategoryRepository.InsertManyAsync(request.defaultCategories, cancellationToken);
            var listOfIds = request.defaultCategories.Select(c => c.Id).ToList();

            return listOfIds;
        }
    }
}
