﻿using AutoMapper;
using FarmProductionAPI.Core.Commands.ProductAttributeCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductAttributeHandler
{
    public class SaveProductAttributeHandler : ICommandHandler<SaveProductAttributeCommand, ResponseResultAPI<ProductAttributeDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductAttribute> _repository;

        public SaveProductAttributeHandler(IMapper mapper, ILogger logger, IRepository<ProductAttribute> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductAttributeDTO>> Handle(SaveProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ProductAttribute = new ProductAttribute();
                if (request.Id.HasValue)
                {
                    ProductAttribute = await _repository.GetById(request.Id.Value);
                    if (ProductAttribute is not null)
                    {
                        await _repository.Update(_mapper.Map<ProductAttribute>(request));
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        return new ResponseResultAPI<ProductAttributeDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                ProductAttribute = _mapper.Map<ProductAttribute>(request);
                await _repository.CreateOneAsync(ProductAttribute, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<ProductAttributeDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<ProductAttributeDTO>(ProductAttribute),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<ProductAttributeDTO>()
                {
                    Code = "500",
                    Data = null,
                    Message = ex.Message,
                    MessageEX = ex.ToString()
                };
            }
        }
    }
}
