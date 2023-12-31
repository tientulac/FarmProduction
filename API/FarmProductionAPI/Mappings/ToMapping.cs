﻿using AutoMapper;
using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Commands.ProductAttributeCommand;
using FarmProductionAPI.Core.Commands.RoleCommand;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;

namespace FarmProductionAPI.Mappings
{
    public class ToMapping : Profile
    {
        public ToMapping()
        {
            CreateMap<SaveBrandCommand, Brand>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.Image));

            CreateMap<Brand, BrandDTO>();

            CreateMap<ProductImage, ProductImageDTO>();

            CreateMap<ProductDescription, ProductDescriptionDTO>();

            CreateMap<SaveCategoryCommand, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<SaveProductAttributeCommand, ProductAttribute>();
            CreateMap<ProductAttribute, ProductAttributeDTO>();

            CreateMap<SaveProductCommand, Product>();
            CreateMap<Product, ProductDTO>();

            CreateMap<SaveUserAccountCommand, UserAccount>();
            CreateMap<UserAccount, UserAccountDTO>();

            CreateMap<SaveRoleCommand, Role>();
            CreateMap<Role, RoleDTO>();
        }
    }
}
