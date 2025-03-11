using FluentValidation;

namespace App.Application.Features.Products.Update;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x=>x.CategoryId).GreaterThan(0).WithMessage("Kategori Id belirtilmelidir.");
    }
}