using BudgetManager.Shared.Models.MongoDB.Models;
using System.Collections.Generic;

namespace BudgetManager.DataAccess.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetEmployees();
        EmployeeModel AddEmployee(string firstName, string lastName);

    }
}
