using System;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using AutoMapper;

namespace ApiEcommerce.Mapping;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap(); 
        CreateMap<Product, UpdateProductDto>().ReverseMap(); 
}
}
