using FluentValidation;
using Library_API.Dtos;

namespace Library_API.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(6).WithMessage("Password must have 6 characters.");
    }
}