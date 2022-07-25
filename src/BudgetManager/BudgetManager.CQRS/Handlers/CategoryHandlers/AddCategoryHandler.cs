using AutoMapper;
using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.SDK.DTO.CategoryDTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, string>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public AddCategoryHandler(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            AddCategoryDTO requestCategory = request.category;
            Category mappedCategory = _mapper.Map<Category>(requestCategory);
            await _categoryRepository.InsertOneAsync(mappedCategory, cancellationToken);
            return mappedCategory.Id.ToString();
        }
    }
}
