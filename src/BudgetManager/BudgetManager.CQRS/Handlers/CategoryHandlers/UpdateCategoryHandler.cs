using AutoMapper;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryHandler, Unit>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(UpdateCategoryHandler request, CancellationToken cancellationToken)
        {
            
        }
    }
}
