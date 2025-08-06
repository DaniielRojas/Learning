using System;
using System.ComponentModel.DataAnnotations;
using Learning.Common.Constants;

namespace Learning.Security.Validators
{
    public class AgeRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                var today = DateTime.Today;

                if (birthDate > today)
                    return new ValidationResult("La fecha de nacimiento no puede ser futura.");

                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;

                if (age < SecurityConstant.AGE_MIN || age > SecurityConstant.AGE_MAX)
                {
                    return new ValidationResult($"La edad debe estar entre {SecurityConstant.AGE_MIN} y {SecurityConstant.AGE_MAX} años.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("La fecha de nacimiento no es válida.");
        }
    }
}
