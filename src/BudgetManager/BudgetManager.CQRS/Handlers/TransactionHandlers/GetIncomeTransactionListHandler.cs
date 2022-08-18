﻿using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetIncomeTransactionListHandler : IRequestHandler<GetIncomeTransactionListQuery, IncomePageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetIncomeTransactionListHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IncomePageResponse> Handle(GetIncomeTransactionListQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.incomesPageDto.PageSize;
            long count;
            IEnumerable<Transaction> incomeTransactions;

            FilterDefinitionBuilder<Transaction>? filterBuilder = Builders<Transaction>.Filter;
            FilterDefinition<Transaction>? filter = filterBuilder.Eq(x => x.UserId, request.userId) & filterBuilder.Eq(x => x.TransactionType, OperationType.Income);

            SortDefinitionBuilder<Transaction> sortBuilder = Builders<Transaction>.Sort;
            SortDefinition<Transaction>? sort;

            if (request.incomesPageDto.DateFrom is not null && request.incomesPageDto.DateTo is not null)
            {
                filter &= filterBuilder.Gte(x => x.DateOfTransaction, request.incomesPageDto.DateFrom) &
                    filterBuilder.Lte(x => x.DateOfTransaction, request.incomesPageDto.DateTo);
            }

            if (request.incomesPageDto.CategoriesFilter is not null)
            {
                filter &= filterBuilder.In(x => x.CategoryId, request.incomesPageDto.CategoriesFilter);
            }

            if (request.incomesPageDto.WalletsFilter is not null)
            {
                filter &= filterBuilder.In(x => x.WalletId, request.incomesPageDto.WalletsFilter);
            }

            if (request.incomesPageDto.IsSortByAmount && !request.incomesPageDto.IsSortByDate)
            {
                if (request.incomesPageDto.IsSortDescending)
                {
                    sort = sortBuilder.Descending(x => x.Value);
                }
                else
                {
                   sort = sortBuilder.Ascending(x => x.Value);
                }
            }
            else
            {
                if (request.incomesPageDto.IsSortDescending)
                {
                    sort = sortBuilder.Descending(x => x.DateOfTransaction);
                }
                else
                {
                    sort = sortBuilder.Ascending(x => x.DateOfTransaction);
                }
            }

            (incomeTransactions, count) = await _dataAccess.GetPageListAsync(filter, sort, request.incomesPageDto.PageNumber, pageSize, cancellationToken);

            var result = new IncomePageResponse()
            {
                Incomes = _mapper.Map<IEnumerable<IncomeTransactionResponse>>(incomeTransactions),
                PageInfo = new Pagination.PageInfo(count, request.incomesPageDto.PageNumber, pageSize),
            };

            return result;
        }
    }
}
