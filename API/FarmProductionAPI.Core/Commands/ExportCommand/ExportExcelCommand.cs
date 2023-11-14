using FarmProductionAPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Commands.ExportCommand;
public class ExportExcelCommand<T> : ICommand<ResponseResultAPI<byte[]>>
{
    public List<T>? ListData { get; set; }
}