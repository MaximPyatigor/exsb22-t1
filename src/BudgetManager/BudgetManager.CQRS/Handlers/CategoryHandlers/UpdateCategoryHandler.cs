using AutoMapper;
using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateCategoryObject = request.updateCategoryObject;

            var mappedCategory = _mapper.Map<Category>(updateCategoryObject);

            var filter = Builders<Category>.Filter.Eq(opt => opt.Id, mappedCategory.Id);
            var update = Builders<Category>.Update
                .Set(o => o.Name, mappedCategory.Name)
                .Set(o => o.Limit, mappedCategory.Limit)
                .Set(o => o.LimitPeriod, mappedCategory.LimitPeriod)
                .Set(o => o.SubCategories, mappedCategory.SubCategories)
                .Set(o => o.CategoryType, mappedCategory.CategoryType)
                .Set(o => o.Color, mappedCategory.Color);
            var response = _mapper.Map<CategoryResponse>(await _categoryRepository.UpdateOneAsync(filter, update, cancellationToken));

            return response;
        }
    }
}
