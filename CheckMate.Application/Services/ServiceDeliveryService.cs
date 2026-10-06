using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.ServiceDelivery;
using CheckMate.Application.Errors.ServiceDelivery;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class ServiceDeliveryService : IServiceDeliveryService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceDeliveryService(
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork
            )
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Service>> CreateServiceAsync(ServiceDeliveryCreationDto dto)
        {
            bool serviceExists = await _serviceRepository.VerifyExistanceByNameAsync(dto.Name);

            if (serviceExists)
            {
                return Result<Service>.Failure(ServiceDeliveryErrors.AlreadyExists(dto.Name));
            }

            Service service = new Service
            {
                ServiceName = dto.Name,
                ServiceDescription = dto.Description,
                ServiceUnitPrice = dto.UnitPrice,
            };

            await _serviceRepository.CreateAsync(service);

            await _unitOfWork.SaveChangesAsync();

            return Result<Service>.Success(service);
        }

        public async Task<IEnumerable<Service>> GetAllServices()
        {
            return await _serviceRepository.GetAllAsync();
        }

        public async Task<Result<Service>> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                return Result<Service>.Failure(ServiceDeliveryErrors.NotFound(serviceId));
            }

            return Result<Service>.Success(service);
        }

        public async Task<Result<Service>> UpdateServiceAsync(int serviceId, ServiceDeliveryUpdateDto dto)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId);

            if (service == null)
            {
                return Result<Service>.Failure(ServiceDeliveryErrors.NotFound(serviceId));
            }

            service.ServiceName = dto.Name;
            service.ServiceDescription = dto.Description;
            service.ServiceUnitPrice = dto.UnitPrice;

            await _serviceRepository.UpdateAsync(service);

            await _unitOfWork.SaveChangesAsync();

            return Result<Service>.Success(service);
        }
    }
}
