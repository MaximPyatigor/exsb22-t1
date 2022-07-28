using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionsByPageHandler : IRequestHandler<GetTransactionsByPageQuery, IEnumerable<Transaction>>
    {
        private readonly IBaseRepository<Transaction> _repository;

        public GetTransactionsByPageHandler(IBaseRepository<Transaction> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByPageQuery request, CancellationToken cancellationToken)
        {
            const int itemsPerPage = 5;
            int pageNumber = request.pageNumber;
            var transactions = await _repository.GetAllAsync(cancellationToken);

            return transactions.Skip(itemsPerPage * (pageNumber - 1)).Take(itemsPerPage);
        }
    }
}
