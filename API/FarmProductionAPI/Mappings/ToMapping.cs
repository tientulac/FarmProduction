using AutoMapper;
using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Commands.OrderCommand;
using FarmProductionAPI.Core.Commands.OrderItemCommand;
using FarmProductionAPI.Core.Commands.ProducerCommand;
using FarmProductionAPI.Core.Commands.ProductAttributeCommand;
using FarmProductionAPI.Core.Commands.ProductDescriptionCommand;
using FarmProductionAPI.Core.Commands.ProductImageCommand;
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

            CreateMap<SaveCategoryCommand, Category>();
            CreateMap<Category, ParentCategoryDTO>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<SaveProductAttributeCommand, ProductAttribute>();
            CreateMap<ProductAttribute, ProductAttributeDTO>();

            CreateMap<SaveProductDescriptionCommand, ProductDescription>();
            CreateMap<ProductDescription, ProductDescriptionDTO>();

            CreateMap<SaveProductCommand, Product>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductDesciptions, otps => otps.MapFrom(src => src.ProductDescriptions));

            CreateMap<SaveUserAccountCommand, UserAccount>();
            CreateMap<UpdateInfoCommand, UserAccount>();
            CreateMap<UserAccount, UserAccountDTO>();

            CreateMap<SaveRoleCommand, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<SaveProductImageCommand, ProductImage>();
            CreateMap<ProductImage, ProductImageDTO>();

            CreateMap<SaveProducerCommand, Producer>();
            CreateMap<Producer, ProducerDTO>();

            CreateMap<SaveOrderCommand, Order>();
            CreateMap<Order, OrderDTO>();

            CreateMap<SaveOrderItemCommand, OrderItem>();
            CreateMap<OrderItem, OrderItemDTO>();
        }
    }
}
