using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly IBaseRepository<Transaction> _dataAccess;

        public DeleteTransactionHandler(IBaseRepository<Transaction> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            bool isDeleted = await _dataAccess.DeleteByIdAsync(request.id, cancellationToken);
            return isDeleted;
        }
    }
}
