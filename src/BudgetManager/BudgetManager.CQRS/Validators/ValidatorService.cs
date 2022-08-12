using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Validators
{
    public class ValidatorService
    {
        public static string GetErrorMessage(ValidationResult result)
        {
            StringBuilder stringBuilder = new ();

            foreach (var failure in result.Errors)
            {
                stringBuilder.AppendLine($"Property  '{failure.PropertyName}'  failed validation. Error was: " + failure.ErrorMessage);
            }

            return stringBuilder.ToString();
        }
    }
}
