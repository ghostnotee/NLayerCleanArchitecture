using AutoMapper;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;

namespace Services.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, expression => expression.MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, expression => expression.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}