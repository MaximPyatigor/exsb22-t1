using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Shared.DataAccess.MongoDB.DbContexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<TCollection> GetCollection<TCollection>(string name);
    }
}
