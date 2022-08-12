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
    public class GetTransactionListByReportRequestHandler : IRequestHandler<GetTransactionListByReportRequestQuery, IEnumerable<Transaction>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetTransactionListByReportRequestHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionListByReportRequestQuery request, CancellationToken cancellationToken)
        {
            var userFilter = Builders<Transaction>.Filter.Eq(t => t.UserId, request.UserId);
            var walletFilter = Builders<Transaction>.Filter.Eq(t => t.WalletId, request.ReportRequestInfo.WalletId);
            var dateFromFilter = Builders<Transaction>.Filter.Gte(t => t.DateOfTransaction, request.ReportRequestInfo.DateFrom);
            var dateToFilter = Builders<Transaction>.Filter.Lte(t => t.DateOfTransaction, request.ReportRequestInfo.DateTo);
            var categoriesFilter = Builders<Transaction>.Filter.In(t => t.CategoryId, request.ReportRequestInfo.IncomeCategoryIds);
            var transactionTypeFilter = Builders<Transaction>.Filter.Eq(t => t.TransactionType, request.TransactionType);

            var filter = Builders<Transaction>.Filter.And(userFilter, walletFilter, dateFromFilter, dateToFilter, categoriesFilter, transactionTypeFilter);

            return await _dataAccess.FilterBy(filter, cancellationToken);
        }
    }
}
