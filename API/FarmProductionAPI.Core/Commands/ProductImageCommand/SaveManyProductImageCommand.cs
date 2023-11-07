using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Commands.ProductImageCommand
{
    public class SaveManyProductImageCommand : ICommand<ResponseResultAPI<List<ProductImageDTO>>>
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public List<string>? Images { get; set; }
    }
}
