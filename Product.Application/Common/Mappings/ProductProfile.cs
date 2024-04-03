using AutoMapper;
using Product.Application.Features.Commands.AddProduct;
using Product.Application.Features.Queries;
using Product.Application.Features.Queries.GetAllProduct;

namespace Product.Application.Common.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Domain.Entities.Product, AddProductCommand>().ReverseMap();
        CreateMap<Domain.Entities.Product, GetProductResponse>().ReverseMap();
        CreateMap<Domain.Entities.Product, GetAllProductsResponse>().ReverseMap();

    }

}
