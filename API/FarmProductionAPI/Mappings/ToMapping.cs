using AutoMapper;
using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Commands.CategoryCommand;
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
        }
    }
}
