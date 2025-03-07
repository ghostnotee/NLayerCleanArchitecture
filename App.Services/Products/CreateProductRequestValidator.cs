using FluentValidation;
using Repositories.Products;

namespace Services.Products;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;

    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün ismi gereklidir.")
            .Length(3, 10).WithMessage("Ürün ismi 3-10 karakter arasında olmalıdır.")
            .Must(MustUniqueName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");
        RuleFor(x => x.Stock).InclusiveBetween(1, 100).WithMessage("Stok 1-100 arasında olmalıdır.");
    }

    private bool MustUniqueName(string name)
    {
        return !_productRepository.Where(x => x.Name == name).Any();
    }
}