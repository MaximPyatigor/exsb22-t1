using AutoMapper;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletCategoriesListHandler : IRequestHandler<GetWalletCategoriesListQuery, IEnumerable<WalletCategoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;

        public GetWalletCategoriesListHandler(IMapper mapper, IMediator mediator, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<WalletCategoryResponse>> Handle(GetWalletCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<Transaction>.Filter;
            var filter = filterBuilder.Eq(x => x.UserId, request.userId) &
                filterBuilder.Eq(x => x.WalletId, request.walletCategoriesDTO.WalletId) &
                filterBuilder.Eq(x => x.TransactionType, request.walletCategoriesDTO.CategoryType);

            var categories = await _transactionRepository.GetWalletDistinctCategoryIdListAsync(filter, cancellationToken);

            var listOfCategories = new List<WalletCategoryResponse>();

            foreach (var category in categories)
            {
                var categoryResponse = await _mediator.Send(new GetOneCategoryQuery(request.userId, category), cancellationToken);
                listOfCategories.Add(_mapper.Map<WalletCategoryResponse>(categoryResponse));
            }

            return listOfCategories;
        }
    }
}
