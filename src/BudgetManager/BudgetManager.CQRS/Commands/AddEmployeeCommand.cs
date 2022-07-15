using BudgetManager.Shared.Models.MongoDB.Models;
using MediatR;

namespace EmployeeManagementLibrary.Commands
{
    public record AddEmployeeCommand(string FirstName,string LastName):IRequest<EmployeeModel>;
    
}
