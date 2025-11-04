using System.ComponentModel.DataAnnotations;

namespace ComarchCwiczenia.Client.Common;

public class PasswordValidation : ValidationAttribute
{
    private readonly int _minLength;
    private readonly int _maxLength;
    private readonly bool _requireDigit;
    private readonly bool _requireLowerCase;
    private readonly bool _requireUpperCase;
    private readonly bool _requireNonAlphanumeric;

    public PasswordValidation(int minLength, int maxLength, bool requireDigit,
        bool requireLowerCase, bool requireUpperCase, bool requireNonAlphanumeric)
    {
        _minLength = minLength;
        _maxLength = maxLength;
        _requireDigit = requireDigit;
        _requireLowerCase = requireLowerCase;
        _requireUpperCase = requireUpperCase;
        _requireNonAlphanumeric = requireNonAlphanumeric;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? password = value as string;

        if (password.Length < _minLength || (_maxLength > 0 && password.Length > _maxLength))
            return new ValidationResult($"The password must be between {_minLength} and {_maxLength} characters long.");

        if (_requireDigit && !password.Any(char.IsAsciiDigit))
        {
            return new ValidationResult("Password must contain at least one digit.");
        }

        if (_requireLowerCase && !password.Any(char.IsLower))
        {
            return new ValidationResult("Password must contain at least one lower letter.");
        }

        if (_requireUpperCase && !password.Any(char.IsUpper))
        {
            return new ValidationResult("Password must contain at least one upper letter.");
        }

        if (_requireNonAlphanumeric && password.All(char.IsLetterOrDigit))
        {
            return new ValidationResult("Password must contain at least one non-alphanumeric character.");
        }

        return ValidationResult.Success;
    }
}
