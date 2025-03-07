using AutoMapper;
using Repositories.Products;
using Services.Products;
using Services.Products.Create;
using Services.Products.Update;

namespace Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}