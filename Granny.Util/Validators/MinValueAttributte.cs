using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.Util.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly string _minValue;

        private readonly Type _type;

        public MinValueAttribute(Type type, string minValue)
        {
            _type = type;
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_type == typeof(decimal))
            {
                decimal minValue = decimal.Parse(_minValue);
                if ((decimal)value < decimal.Parse(_minValue)) return new ValidationResult($"Field {validationContext.DisplayName} greater than minimum value {minValue}");
                return ValidationResult.Success;
            }
            return new ValidationResult("Not a supported type");
        }
    }
}
