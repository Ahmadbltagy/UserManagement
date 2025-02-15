using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Application.Validators;

public class ValidEnumAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public ValidEnumAttribute(Type enumType)
    {
        _enumType = enumType;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !Enum.IsDefined(_enumType, value))
        {
            return new ValidationResult($"Invalid value for {validationContext.MemberName}.");
        }
        return ValidationResult.Success;
    }
}
