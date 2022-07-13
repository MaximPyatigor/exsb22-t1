using API.Settings;
using Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly IMongoCollection<Transaction> transactionsCollection;

        public TransactionRepository(IOptions<MongoDbConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

            transactionsCollection = mongoDatabase.GetCollection<Transaction>(
                options.Value.TransactionsCollectionName);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync() =>
            await transactionsCollection.Find(_ => true).ToListAsync();

        public async Task<Transaction?> GetAsync(Guid id) =>
            await transactionsCollection.Find(transaction => transaction.Id == id).FirstOrDefaultAsync();

        public async Task InsertAsync(Transaction item) =>
            await transactionsCollection.InsertOneAsync(item);

        public async Task RemoveAsync(Guid id) =>
            await transactionsCollection.DeleteOneAsync(transaction => transaction.Id == id);

        public async Task UpdateAsync(Guid id, Transaction item) =>
            await transactionsCollection.ReplaceOneAsync(transaction => transaction.Id == id, item);
    }
}
