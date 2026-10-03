using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IServiceDeliveryService
    {
        Task<Result<Service>> CreateServiceAsync(ServiceDeliveryCreationDto dto);

        Task<Result<Service>> GetServiceByIdAsync(int serviceId);

        Task<IEnumerable<Service>> GetAllServices();

        Task<Service> UpdateService();
    }
}
