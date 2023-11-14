using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Queries.ProducerQuery
{
    public class GetListProducerQuery : IRequest<ResponseResultAPI<List<ProducerDTO>>>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
