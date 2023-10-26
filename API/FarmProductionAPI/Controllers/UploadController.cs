using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Queries.BrandQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseResultAPI<bool>> UploadImage(IFormFile file, CancellationToken cancellationToken)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return new ResponseResultAPI<bool>()
                    {
                        Code = "200",
                        Data = true,
                        Message = path + $"\\{file.FileName}.jpg"
                    };
                }
                else
                {
                    return new ResponseResultAPI<bool>()
                    {
                        Code = "500",
                        Data = false,
                        Message = path
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<bool>()
                {
                    Code = "500",
                    Data = false,
                    Message = ex.Message,
                    MessageEX = ex.ToString()
                };
            }
        }
    }
}
