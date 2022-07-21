using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.CQRS.Responses.CategoryResponses;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, CategoryResponse>
    {
        public Task<CategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
