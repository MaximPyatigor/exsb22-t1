using BudgetManager.Shared.Models.MongoDB.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Shared.DataAccess.MongoDB
{
    public class MongoDbContext
    {
        public readonly IMongoCollection<Category> Categories;
        public readonly IMongoCollection<Notification> Notifications;
        public readonly IMongoCollection<Transaction> Transactions;
        public readonly IMongoCollection<User> Users;
        public readonly IMongoCollection<Wallet> Wallets;

        public MongoDbContext(IOptions<ExadelBudgetDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            Categories = mongoDatabase.GetCollection<Category>(
                databaseSettings.Value.CategoryCollectionName);
            Notifications = mongoDatabase.GetCollection<Notification>(
                databaseSettings.Value.CategoryCollectionName);
            Transactions = mongoDatabase.GetCollection<Transaction>(
                databaseSettings.Value.CategoryCollectionName);
            Users = mongoDatabase.GetCollection<User>(
                databaseSettings.Value.CategoryCollectionName);
            Wallets = mongoDatabase.GetCollection<Wallet>(
                databaseSettings.Value.CategoryCollectionName);
        }
    }
}
