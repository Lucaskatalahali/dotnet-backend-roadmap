
using FluentValidation;
using Library_API.Dtos;

namespace Library_API.Validators;

public class PatchMemberDtoValidator : AbstractValidator<PatchMemberDto>
{
    public PatchMemberDtoValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .When(m => m.Name is not null)
            .WithMessage("Name cannot be empty.")
            .MaximumLength(50)
            .When(m => m.Name is not null)
            .WithMessage("Name length cannot exceed 50 characters."); 

        RuleFor(m => m.Email)
            .NotEmpty()
            .When(m => m.Email is not null)
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .When(m => m.Email is not null)
            .WithMessage("A valid email is required.");
    }
}