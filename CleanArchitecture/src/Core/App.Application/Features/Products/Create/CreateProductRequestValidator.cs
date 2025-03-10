using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;

    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name is required.")
            .Length(3, 10).WithMessage("Product Name must be between 3 and 10 characters.");
            //.Must(MustUniqueName).WithMessage("Product Name already exists.");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.Stock).InclusiveBetween(1, 100).WithMessage("Stock must be between 1 and 100.");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category Id must be greater than zero.");
    }

    private bool MustUniqueName(string name)
    {
        return !_productRepository.Where(x => x.Name == name).Any();
    }
}