using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UrphaCapital.Application.Filters;

public class PasswordAttribute : ValidationAttribute
{
    public int MinimumLength { get; set; } = 8;

    public PasswordAttribute(int minimumLength = 8)
    {
        MinimumLength = minimumLength;
        ErrorMessage = $"Password must be at least {MinimumLength} characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string password = value as string;

        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            return new ValidationResult(ErrorMessage);
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return new ValidationResult("Password must contain at least one uppercase letter.");
        }
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return new ValidationResult("Password must contain at least one lowercase letter.");
        }
        if (!Regex.IsMatch(password, @"\d"))
        {
            return new ValidationResult("Password must contain at least one digit.");
        }
        if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?:{ }|<>]"))
        {
            return new ValidationResult("Password must contain at least one special character.");
        }
        return ValidationResult.Success;
    }
}

