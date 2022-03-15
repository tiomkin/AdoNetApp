using System;
using System.ComponentModel.DataAnnotations;

namespace AdoNetApp.WebApp.Attributes
{
	public class StringRangeAttribute : ValidationAttribute
	{
		public Type EnumType { get; set; }

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (Enum.IsDefined(EnumType, value.ToString()))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult("Wrong value for enum.");
		}
	}
}
