﻿using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;

namespace BudgetManager.DataAccess.MongoDbAccess.Interfaces
{
    public interface ICurrencyRepository : IBaseRepository<Currency>
    {
    }
}
