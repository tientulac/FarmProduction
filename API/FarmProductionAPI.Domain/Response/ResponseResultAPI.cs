using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Response
{
    public class ResponseResultAPI<T>
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? MessageEX { get; set; }
        public T? Data { get; set; }
    }
}
