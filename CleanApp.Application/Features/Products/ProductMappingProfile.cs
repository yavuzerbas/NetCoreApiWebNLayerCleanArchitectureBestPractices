using AutoMapper;
using CleanApp.Application.Features.Products.Create;
using CleanApp.Application.Features.Products.Dto;
using CleanApp.Application.Features.Products.Update;
using CleanApp.Domain.Entities;

namespace CleanApp.Application.Features.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src =>
                        src.Name.ToLowerInvariant()
                    ));
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src =>
                        src.Name.ToLowerInvariant()
                    ));
        }
    }
}
