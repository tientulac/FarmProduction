using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Commands.OrderItemCommand
{
    public class InsertOrderItemCommand : ICommand<ResponseResultAPI<List<OrderItemDTO>>>
    {
        public string? ProductName { get; set; }
        public string? Code { get; set; }
        public string? Color { get; set; }
        public Guid? OrderId { get; set; }
    }
}
