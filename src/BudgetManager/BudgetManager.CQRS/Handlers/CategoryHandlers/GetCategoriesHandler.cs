using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponse>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public GetCategoriesHandler(IBaseRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public Task<List<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allCategoriesFromDb = this._categoryRepository.AsQueryable().ToList();
            var listOfResponseCategories = new List<CategoryResponse>();
            foreach (var category in allCategoriesFromDb)
            {
                listOfResponseCategories.Add(new CategoryResponse()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Limit = category.Limit,
                    LimitPeriod = category.LimitPeriod,
                    SubCategories = category.SubCategories,
                    CategoryType = category.CategoryType,
                    Color = category.Color,
                });
            }

            return Task.FromResult(listOfResponseCategories);
        }
    }
}
