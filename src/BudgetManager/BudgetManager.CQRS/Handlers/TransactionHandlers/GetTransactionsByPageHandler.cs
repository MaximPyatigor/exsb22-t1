using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.DataAccess.MongoDB.DatabaseSettings;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionsByPageHandler : IRequestHandler<GetTransactionsByPageQuery, IEnumerable<Transaction>>
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Transaction> _transactionsCollection;
        
        public GetTransactionsByPageHandler(IMongoDbSettings settings, IMongoClient client)
        {
            _client = client;
            _database = client.GetDatabase(settings.DatabaseName);
            _transactionsCollection = _database.GetCollection<Transaction>(nameof(Transaction));
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByPageQuery request, CancellationToken cancellationToken)
        {
            const int itemsPerPage = 5;
            var query = _transactionsCollection.Find(Builders<Transaction>.Filter.Empty);

            return await query.Skip(itemsPerPage * (request.pageNumber - 1))
                              .Limit(itemsPerPage)
                              .ToListAsync(cancellationToken);
        }
    }
}
