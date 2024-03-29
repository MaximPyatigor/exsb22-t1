﻿using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddCategoryDTO
    {
        public string Name { get; set; }

        public decimal Limit { get; set; }

        public LimitPeriods LimitPeriod { get; set; } = LimitPeriods.None;

        public OperationType CategoryType { get; set; } = OperationType.None;

        public string Color { get; set; } = null;
    }
}
