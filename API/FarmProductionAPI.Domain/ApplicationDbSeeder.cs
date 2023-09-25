using AutoMapper;
using FarmProductionAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Domain
{
    public class ApplicationDbSeeder
    {
        private readonly ILogger _logger;

        private readonly IMapper _mapper;


        private readonly DataContext _dataContext;

        public ApplicationDbSeeder(ILogger logger, IMapper mapper, DataContext dataContext)
        {
            _logger = logger;
            _mapper = mapper;
            _dataContext = dataContext;
        }
    }

}
