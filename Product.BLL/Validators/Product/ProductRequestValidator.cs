using FluentValidation;
using Product.Domain.Requests;

namespace Product.BLL.Validators.Product;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("The name must not be null")
            .NotEmpty().WithMessage("The name must not be empty");
        
        RuleFor(x => x.Description)
            .NotNull().WithMessage("The name must not be null")
            .NotEmpty().WithMessage("The name must not be empty");
        
        RuleFor(x => x.Price)
            .NotNull().WithMessage("The name must not be null")
            .NotEmpty().WithMessage("The name must not be empty")
            .Must(x => x > 0);
    }
}