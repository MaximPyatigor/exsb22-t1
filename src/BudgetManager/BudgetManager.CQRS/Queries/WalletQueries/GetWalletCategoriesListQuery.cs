using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetWalletCategoriesListQuery(Guid userId, WalletCategoriesDTO walletCategoriesDTO) : IRequest<IEnumerable<WalletCategoryResponse>>;
}
