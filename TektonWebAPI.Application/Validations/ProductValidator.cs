using FluentValidation;

namespace TektonWebAPI.Application.Validations;

public class ProductValidator : AbstractValidator<ProductRequestDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.Status).InclusiveBetween(0, 1);
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.Price).InclusiveBetween(1, 100000000);
    }
}