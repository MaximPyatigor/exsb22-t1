using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class AddUserPayerHandler : IRequestHandler<AddUserPayerCommand, string?>
    {
        private readonly IBaseRepository<User> _dataAccess;

        public AddUserPayerHandler(IBaseRepository<User> dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<string?> Handle(AddUserPayerCommand request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            var payer = request.name;

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Push(u => u.Payers, payer);

            var result = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);

            return result is not null ? payer : null;
        }
    }
}
