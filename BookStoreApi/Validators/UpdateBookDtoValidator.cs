

using BookStoreApi.Dtos;
using FluentValidation;

namespace BookStoreApi.Validators;

public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("The title cannot exceed 10 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .LessThanOrEqualTo(100000).WithMessage("The maximum allowed price is 100000");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Invalid Category Id");

    }
}