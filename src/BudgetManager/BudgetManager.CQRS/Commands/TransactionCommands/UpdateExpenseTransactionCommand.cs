using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record UpdateExpenseTransactionCommand(Guid userId, UpdateExpenseTransactionDTO updateExpenseDto) : IRequest<ExpenseTransactionResponse>;
}
