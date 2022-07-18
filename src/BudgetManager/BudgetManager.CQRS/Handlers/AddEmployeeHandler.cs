using BudgetManager.DataAccess.Interfaces;
using BudgetManager.Shared.Models.MongoDB.Models;
using EmployeeManagementLibrary.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementLibrary.Handlers
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, EmployeeModel>
    {
        private readonly IEmployeeService _dataAccess;

        public AddEmployeeHandler(IEmployeeService dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public  Task<EmployeeModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.AddEmployee(request.FirstName, request.LastName));
        }
    }
}
