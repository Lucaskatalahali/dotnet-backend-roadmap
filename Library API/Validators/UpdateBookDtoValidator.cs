using FluentValidation;
using Library_API.Dtos;

namespace Library_API.Validators;

public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required while updating.")
            .MaximumLength(50).WithMessage("Title length cannot exceed 50 characters.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(50).WithMessage("Author's name cannot exceed 50 charachers.");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Length(13).WithMessage("ISBN must have 13 digits");

        RuleFor(x => x.PublishedYear)
            .NotEmpty().WithMessage("Year cannot be empty")
            .GreaterThan(0).WithMessage("Publish year must be a valid year")
            .LessThanOrEqualTo(DateTime.Now.Year); 

        RuleFor(x => x.IsAvailable)
            .NotNull().WithMessage("You must inform if the book is available or not.");
    }
}