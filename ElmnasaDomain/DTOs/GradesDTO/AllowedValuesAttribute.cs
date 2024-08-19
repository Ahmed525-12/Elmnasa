using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.GradesDTO
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedValuesAttribute(params string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is string stringValue))
            {
                return new ValidationResult("Invalid value.");
            }

            if (_allowedValues.Contains(stringValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The value '{stringValue}' is not valid. Allowed values are: {string.Join(", ", _allowedValues)}.");
        }
    }
}