using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ExportCommand;
public class ExportExcelCommand<T> : ICommand<ResponseResultAPI<byte[]>> { };