using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Queries.BrandQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System;
using NetTopologySuite.Index.HPRtree;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                        Message = $"{file.FileName}"
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

        [HttpGet]
        public IActionResult GetImage(string imgName)
        {
            try
            {
                string imagePath = $"UploadedFiles/{imgName}";

                if (System.IO.File.Exists(imagePath))
                {
                    string contentType = "image/jpeg"; 

                    return File(System.IO.File.OpenRead(imagePath), contentType);
                }
                else
                {
                    return NotFound(); 
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ResponseResultAPI<List<string>>> UploadImages(IEnumerable<IFormFile> files, CancellationToken cancellationToken)
        {
            string path = "";
            try
            {
                var listImage = new List<string>();
                if (files.Any())
                {
                    foreach (var item in files)
                    {
                        if (item.Length > 0)
                        {
                            path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            using (var fileStream = new FileStream(Path.Combine(path, item.FileName), FileMode.Create))
                            {
                                await item.CopyToAsync(fileStream);
                            }
                            listImage.Add(item.FileName);
                        }
                    }                   
                }
                return new ResponseResultAPI<List<string>>()
                {
                    Code = "200",
                    Data = listImage.Distinct().ToList(),
                    Message = $"Upload Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<string>>()
                {
                    Code = "500",
                    Data = null,
                    Message = ex.Message,
                    MessageEX = ex.ToString()
                };
            }
        }

        [HttpGet]
        public List<string> GetAllImages()
        {
            try
            {
                return Directory.GetFiles("UploadedFiles").Select(x => x.Replace("UploadedFiles\\", "")).ToList();
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                throw ex;
            }
        }
    }
}
