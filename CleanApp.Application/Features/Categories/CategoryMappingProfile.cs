using AutoMapper;
using CleanApp.Application.Features.Categories.Create;
using CleanApp.Application.Features.Categories.Dto;
using CleanApp.Application.Features.Categories.Update;
using CleanApp.Domain.Entities;

namespace CleanApp.Application.Features.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<CategoryWithProductsDto, Category>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src =>
                        src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src =>
                        src.Name.ToLowerInvariant()));
        }
    }
}
