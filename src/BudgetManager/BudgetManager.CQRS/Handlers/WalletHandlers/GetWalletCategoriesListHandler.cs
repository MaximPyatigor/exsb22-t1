using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletCategoriesListHandler : IRequestHandler<GetWalletCategoriesListQuery, IEnumerable<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;

        public GetWalletCategoriesListHandler(IMediator mediator, ITransactionRepository transactionRepository)
        {
            _mediator = mediator;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Guid>> Handle(GetWalletCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<Transaction>.Filter;
            var filter = filterBuilder.Eq(x => x.UserId, request.userId) &
                filterBuilder.Eq(x => x.WalletId, request.walletCategoriesDTO.WalletId) &
                filterBuilder.Eq(x => x.TransactionType, request.walletCategoriesDTO.TransactionType);

            var result = await _transactionRepository.GetWalletDistinctCategoryIdListAsync(filter, cancellationToken);
            return result;
        }
    }
}
