using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBookLibrary.Helpers
{
    // Validation customize for the first letter must be capital 
    public class FirstLetterCapitalAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstletter = value.ToString()[0].ToString();

            if (firstletter != firstletter.ToUpper())
            {
                return new ValidationResult("The first letter must be uppercase");
            }


            return ValidationResult.Success;
        }
    }
}
