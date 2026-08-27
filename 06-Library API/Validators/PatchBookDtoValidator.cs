using FluentValidation;
using Library_API.Dtos;

namespace Library_API.Validators;

public class PatchBookDtoValidator : AbstractValidator<PatchBookDto>
{
    public PatchBookDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .When(x => x.Title is not null)
            .WithMessage("Title cannot be empty.")
            .MaximumLength(50)
            .When(x => x.Title is not null)
            .WithMessage("Title length cannot exceed 50 characters.");

        RuleFor(x => x.Author)
            .NotEmpty()
            .When(x => x.Author is not null)
            .WithMessage("Author cannot be empty.")
            .MaximumLength(50)
            .When(x => x.Author is not null)
            .WithMessage("Author's name cannot exceed 50 characters.");

        RuleFor(x => x.ISBN)
            .NotEmpty()
            .When(x => x.ISBN is not null)
            .WithMessage("ISBN cannot be empty.")
            .Length(13)
            .When(x => x.ISBN is not null)
            .WithMessage("ISBN must have 13 digits.");

        RuleFor(x => x.PublishedYear)
            .GreaterThan(0)
            .When(x => x.PublishedYear is not null)
            .WithMessage("Published year must be a valid year.")
            .LessThanOrEqualTo(DateTime.Now.Year)
            .When(x => x.PublishedYear is not null)
            .WithMessage("Published year cannot be in the future.");
    }
}