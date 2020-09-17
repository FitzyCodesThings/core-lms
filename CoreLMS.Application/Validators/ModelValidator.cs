using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreLMS.Application.Validators
{
    public static class ModelValidator
    {
        public static void ValidateModel(object model)
        {
            var ctx = new ValidationContext(model, null, null);

            Validator.ValidateObject(model, ctx, true);
        }

        private static IList<ValidationResult> GetModelValidationResults(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
