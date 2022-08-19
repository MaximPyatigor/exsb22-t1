using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetExpenseTransactionListHandler : IRequestHandler<GetExpenseTransactionListQuery, ExpensePageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetExpenseTransactionListHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<ExpensePageResponse> Handle(GetExpenseTransactionListQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.expensesPageDto.PageSize;
            long count;
            IEnumerable<Transaction> expenseTransactions;

            FilterDefinitionBuilder<Transaction>? filterBuilder = Builders<Transaction>.Filter;
            FilterDefinition<Transaction>? filter = filterBuilder.Eq(x => x.UserId, request.userId) & filterBuilder.Eq(x => x.TransactionType, OperationType.Expense);

            SortDefinitionBuilder<Transaction> sortBuilder = Builders<Transaction>.Sort;
            SortDefinition<Transaction>? sort;

            if (request.expensesPageDto.DateFrom is not null && request.expensesPageDto.DateTo is not null)
            {
                filter &= filterBuilder.Gte(x => x.DateOfTransaction, request.expensesPageDto.DateFrom) &
                    filterBuilder.Lte(x => x.DateOfTransaction, request.expensesPageDto.DateTo);
            }

            if (request.expensesPageDto.CategoriesFilter is not null)
            {
                filter &= filterBuilder.In(x => x.CategoryId, request.expensesPageDto.CategoriesFilter);
            }

            if (request.expensesPageDto.WalletsFilter is not null)
            {
                filter &= filterBuilder.In(x => x.WalletId, request.expensesPageDto.WalletsFilter);
            }

            if (request.expensesPageDto.PayersFilter is not null)
            {
                filter &= filterBuilder.In(x => x.Payer, request.expensesPageDto.PayersFilter);
            }

            if (request.expensesPageDto.IsSortByAmount && !request.expensesPageDto.IsSortByDate)
            {
                if (request.expensesPageDto.IsSortDescending)
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
                if (request.expensesPageDto.IsSortDescending)
                {
                    sort = sortBuilder.Descending(x => x.DateOfTransaction);
                }
                else
                {
                    sort = sortBuilder.Ascending(x => x.DateOfTransaction);
                }
            }

            (expenseTransactions, count) = await _dataAccess.GetPageListAsync(filter, sort, request.expensesPageDto.PageNumber, pageSize, cancellationToken);

            var result = new ExpensePageResponse()
            {
                Expenses = _mapper.Map<IEnumerable<ExpenseTransactionResponse>>(expenseTransactions),
                PageInfo = new Pagination.PageInfo(count, request.expensesPageDto.PageNumber, pageSize),
            };

            return result;
        }
    }
}
