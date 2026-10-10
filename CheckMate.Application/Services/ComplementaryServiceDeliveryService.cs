using CheckMate.Application.Common.Results;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class ComplementaryServiceDeliveryService : IComplementaryServiceDeliveryService
    {
        private readonly IComplementaryServiceRepository _complementaryServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComplementaryServiceDeliveryService(
            IComplementaryServiceRepository complementaryServiceRepository,
            IUnitOfWork unitOfWork)
        {
            _complementaryServiceRepository = complementaryServiceRepository;
            _unitOfWork = unitOfWork;
        }
        public Task<Result<ComplementaryService>> CreateComplementaryServiceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ComplementaryService>> GetComplementaryServicesAsync()
        {
            return await _complementaryServiceRepository.GetAllAsync();
        }

        public Task<IEnumerable<ComplementaryService>> GetComplementaryServicesForReservationRoomAsync()
        {
            throw new NotImplementedException();
        }
    }
}
