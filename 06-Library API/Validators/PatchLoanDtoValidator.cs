using FluentValidation;
using Library_API.Dtos;

namespace Library_API.Validators;

public class PatchLoanDtoValidator : AbstractValidator<PatchLoanDto>
{
    public PatchLoanDtoValidator()
    {
        RuleFor(l => l.DueDate)
            .NotEmpty()
            .When(l => l.DueDate is not null)
            .WithMessage("Due date is required.")
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Loan date cannot be in the past.");

        RuleFor(l => l.ReturnedAt)
            .NotEmpty()
            .When(l => l.ReturnedAt is not null)
            .WithMessage("Return date cannot be empty")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Return date cannot be in the future.");
    }
}