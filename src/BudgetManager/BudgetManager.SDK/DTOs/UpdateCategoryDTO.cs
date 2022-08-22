using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class UpdateCategoryDTO
    {
        public string Name { get; set; }

        public decimal Limit { get; set; }

        public LimitPeriods LimitPeriod { get; set; } = LimitPeriods.None;

        public string Color { get; set; }
    }
}
