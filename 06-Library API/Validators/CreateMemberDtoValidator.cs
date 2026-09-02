using FluentValidation;
using Library_API.Dtos;

namespace Library_API.Validators;

public class CreateMemberDtoValidator : AbstractValidator<CreateMemberDto>
{
    public CreateMemberDtoValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name length cannot exceed 50 characters."); 

        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("A valid email is required.");    

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Length(6).WithMessage("Password must have 6 caracters"); 
    }
}