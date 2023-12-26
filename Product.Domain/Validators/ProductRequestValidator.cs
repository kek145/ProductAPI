using FluentValidation;
using Product.Domain.Requests;

namespace Product.Domain.Validators;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(req => req.Name)
            .NotEmpty().WithMessage("Name is not empty")
            .NotNull().WithMessage("Title is not null")
            .MinimumLength(10).WithMessage("Minimum input from 10 to 100 characters")
            .MaximumLength(100).WithMessage("Minimum input from 10 to 100 characters")
            .Must(x => !x.Contains(' ')).WithMessage("Spaces are not allowed in the name");

        RuleFor(req => req.Description)
            .NotEmpty().WithMessage("Name is not empty")
            .NotNull().WithMessage("Title is not null")
            .MinimumLength(10).WithMessage("Minimum input from 10 to 100 characters")
            .MaximumLength(100).WithMessage("Minimum input from 10 to 100 characters");
    }
}