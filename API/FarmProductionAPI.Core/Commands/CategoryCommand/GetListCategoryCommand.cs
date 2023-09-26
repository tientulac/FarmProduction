using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Commands.CategoryCommand
{
    public record GetListCategoryCommand : IRequest<ResponseResultAPI<List<CategoryDTO>>>;
}
