using BudgetManager.DataAccess.Interfaces;
using BudgetManager.Shared.Models.MongoDB.Models;
using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.DataAccess.Services
{
    public class EmployeeService : IEmployeeService
    {
        private List<EmployeeModel> _employees = new(); // C# 9 syntax.

        public EmployeeService()
        {
            _employees.Add(new EmployeeModel { Id = 1, FirstName = "Praveen", LastName = "Raveendran Pillai" });
            _employees.Add(new EmployeeModel { Id = 2, FirstName = "James", LastName = "Roger" });
        }

        public List<EmployeeModel> GetEmployees()
        {
            return _employees;
        }

        public EmployeeModel AddEmployee(string firstName, string lastName)
        {
            EmployeeModel newEmployee = new() { FirstName = firstName, LastName = lastName };
            newEmployee.Id = _employees.Max(x => x.Id) + 1;
            return newEmployee;
        }
    }


}
