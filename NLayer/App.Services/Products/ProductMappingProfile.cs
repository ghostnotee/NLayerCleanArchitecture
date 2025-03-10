using AutoMapper;
using Repositories.Products;
using Services.Products.Create;
using Services.Products.Update;

namespace Services.Products;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name, expression => expression.MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name, expression => expression.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}