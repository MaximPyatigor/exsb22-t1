using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class GetUserPayersHandler : IRequestHandler<GetUserPayersQuery, IEnumerable<string>?>
    {
        private readonly IBaseRepository<User> _dataAccess;

        public GetUserPayersHandler(IBaseRepository<User> dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<IEnumerable<string>?> Handle(GetUserPayersQuery request, CancellationToken cancellationToken)
        {
            var user = await _dataAccess.FindByIdAsync(request.userId, cancellationToken);

            return user?.Payers;
        }
    }
}
