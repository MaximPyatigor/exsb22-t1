﻿using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.PageResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetWalletRecentTransactionsHandler : IRequestHandler<GetWalletRecentTransactionsQuery, PageResponse<RecentTransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetWalletRecentTransactionsHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<PageResponse<RecentTransactionResponse>> Handle(GetWalletRecentTransactionsQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.recentTransactionsPageDTO.PageSize;
            long count;
            IEnumerable<Transaction> recentTransactions;

            FilterDefinitionBuilder<Transaction>? filterBuilder = Builders<Transaction>.Filter;
            FilterDefinition<Transaction>? filter = filterBuilder.Eq(x => x.UserId, request.userId) & filterBuilder.Eq(x => x.WalletId, request.recentTransactionsPageDTO.WalletId);

            SortDefinition<Transaction>? sort = Builders<Transaction>.Sort.Descending(x => x.DateOfTransaction);

            (recentTransactions, count) = await _dataAccess.GetPageListAsync(filter, sort, request.recentTransactionsPageDTO.PageNumber, pageSize, cancellationToken);

            var result = new PageResponse<RecentTransactionResponse>()
            {
                Data = _mapper.Map<IEnumerable<RecentTransactionResponse>>(recentTransactions),
                PageInfo = new SDK.Pagination.PageInfo(count, request.recentTransactionsPageDTO.PageNumber, pageSize),
            };

            return result;
        }
    }
}
