using AutoMapper;
using BudgetManager.CQRS.Commands.SubCategoryCommands;
using BudgetManager.CQRS.Queries.SubCategoryQueries;
using BudgetManager.CQRS.Responses.SubCategoryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CategoryHandlers
{
    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryCommand, SubCategoryResponse>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateSubCategoryHandler(IBaseRepository<User> userRepository, IMediator mediator, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SubCategoryResponse> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategories = _mapper.Map<IEnumerable<Category>>(await _mediator.Send(new GetSubCategoriesQuery(request.userId, request.updateSubCategoryDTO.CategoryId), cancellationToken));
            var subCategory = subCategories.Where(s => s.Id == request.updateSubCategoryDTO.SubCategoryId).FirstOrDefault();
            if (subCategory == null) { throw new KeyNotFoundException("Update SubCategory not found"); }
            subCategory.Name = request.updateSubCategoryDTO.Name;
            subCategory.Color = request.updateSubCategoryDTO.Color;

            var categoryFilter = Builders<Category>.Filter.Eq(u => u.Id, request.updateSubCategoryDTO.CategoryId);
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId)
                & Builders<User>.Filter.ElemMatch(u => u.Categories, categoryFilter);
            var update = Builders<User>.Update.Set(u => u.Categories[-1].SubCategories, subCategories);

            var user = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
            if (user == null) { throw new KeyNotFoundException("Update User not found"); }

            var updatedCategory = user.Categories?.Where(c => c.Id == request.updateSubCategoryDTO.CategoryId).FirstOrDefault();
            if (updatedCategory == null) { throw new KeyNotFoundException("Update Category not found"); }

            var updatedSubCategory = updatedCategory.SubCategories?.Where(s => s.Id == request.updateSubCategoryDTO.SubCategoryId).FirstOrDefault();
            if (updatedSubCategory == null) { throw new KeyNotFoundException("Update SubCategory not found"); }

            return _mapper.Map<SubCategoryResponse>(updatedSubCategory);
        }
    }
}
