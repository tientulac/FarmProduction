using AutoMapper;
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
            CreateMap<SaveBrandCommand, Brand>();
            CreateMap<Brand, BrandDTO>();

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
