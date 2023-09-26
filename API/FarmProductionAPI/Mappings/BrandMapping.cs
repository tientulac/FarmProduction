using AutoMapper;
using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;

namespace FarmProductionAPI.Mappings
{
    public class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<SaveBrandCommand, Brand>();
            CreateMap<Brand, BrandDTO>();
        }
    }
}
