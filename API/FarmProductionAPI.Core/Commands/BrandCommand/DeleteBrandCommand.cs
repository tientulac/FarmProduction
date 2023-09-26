﻿using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Commands.BrandCommand
{
    public record DeleteBrandCommand : ICommand<ResponseResultAPI<BrandDTO>> { 
        public Guid? Id { get; set; }
    };
}
