﻿using AutoMapper;
using BudgetManager.CQRS.Commands.CategoryCommands;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddCategoryHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            Guid userId = request.userId;
            AddCategoryDTO requestCategory = request.category;
            Category mappedCategory = _mapper.Map<Category>(requestCategory);

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Push(u => u.Categories, mappedCategory);

            var result = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);

            return result is not null ? mappedCategory.Id : Guid.Empty;
        }
    }
}
