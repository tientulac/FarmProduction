using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class AttributeDTO
    {
        public List<string>? Colors { get; set; }
        public List<string>? Descriptions { get; set; }
    }
}
