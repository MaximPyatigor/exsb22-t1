using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Model.ReportModels;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Validators
{
    public class GetReportValidator : AbstractValidator<ReportRequest>
    {
        private readonly IBaseRepository<User> _repository;
        private User _user;
        public GetReportValidator(IBaseRepository<User> repository)
        {
            _repository = repository;
            RuleFor(r => r).Must(isIncomeOrCategorySelected).WithMessage("Either income or expense category must be selected");
        }

        public async Task SetUser(Guid userId, CancellationToken cancellationToken)
        {
            _user = await _repository.FindByIdAsync(userId, cancellationToken);
        }

        public bool isIncomeOrCategorySelected(ReportRequest reportRequest)
        {
            return reportRequest.IncomeCategoryIds.Any() || reportRequest.ExpenseCategoryIds.Any();
        }

    }
}