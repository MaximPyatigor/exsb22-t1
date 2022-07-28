using BudgetManager.CQRS.Commands.DefaultCategoryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.DefaultCategoryHandlers
{
    public class AddManyDefaultCategoriesHandler : IRequestHandler<AddManyDefaultCategoriesCommand, IEnumerable<Guid>>
    {
        private readonly IBaseRepository<DefaultCategory> _defaultCategoryRepository;

        public AddManyDefaultCategoriesHandler(IBaseRepository<DefaultCategory> baseRepository)
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
