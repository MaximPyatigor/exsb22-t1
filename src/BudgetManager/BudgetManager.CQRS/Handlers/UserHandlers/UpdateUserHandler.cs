using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = _mapper.Map<User>(request.updateUser);
            var filter = Builders<User>.Filter.Eq(u => u.Id, updateUser.Id);
            var update = Builders<User>.Update
                .Set(u => u.FullName, updateUser.FullName)
                .Set(u => u.DOB, updateUser.DOB)
                .Set(u => u.DefaultCurrency, updateUser.DefaultCurrency)
                .Set(u => u.Categories, updateUser.Categories)
                .Set(u => u.Wallets, updateUser.Wallets)
                .Set(u => u.Notifications, updateUser.Notifications)
                .Set(u => u.DefaultWallet, updateUser.DefaultWallet)
                .Set(u => u.Country, updateUser.Country);
            var result = _mapper.Map<UserResponse>(await _userRepository.UpdateOneAsync(filter, update, cancellationToken));

            return result;
        }
    }
}
