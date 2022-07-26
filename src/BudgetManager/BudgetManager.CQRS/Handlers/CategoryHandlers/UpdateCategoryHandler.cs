using AutoMapper;
using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateCategoryObject = request.updateCategoryObject;
            var existingCategoryObject = await _categoryRepository.FindByIdAsync(updateCategoryObject.Id, cancellationToken);
            if (existingCategoryObject == null)
            {
                return null;
            }

            var mappedCategory = _mapper.Map<Category>(updateCategoryObject);
            var response = await _categoryRepository.ReplaceOneAsync(mappedCategory, cancellationToken);

            return response;
        }
    }
}
