using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.ReservationType;
using CheckMate.Application.Errors.ReservationType;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class ReservationTypeService : IReservationTypeService
    {
        private readonly IReservationTypeRepository _reservationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationTypeService(IReservationTypeRepository reservationTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationTypeRepository = reservationTypeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ReservationType>> CreateReservationTypeAsync(ReservationTypeCreationDto dto)
        {
            bool codeUsed = await _reservationTypeRepository.VerifyCodeExistance(dto.Code);

            if (codeUsed)
                return Result<ReservationType>.Failure(ReservationTypeErrors.CodeAlreadyExists(dto.Code));

            ReservationType type = new()
            {
                ReservationTypeCode = dto.Code,
                Description = dto.Description
            };

            await _reservationTypeRepository.CreateAsync(type);

            await _unitOfWork.SaveChangesAsync();

            return Result<ReservationType>.Success(type);
        }

        public async Task<IEnumerable<ReservationType>> GetAllReservationTypesAsync()
        {
            return await _reservationTypeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ReservationType>> GetAllReservationTypesFromCodeAsync(string code)
        {
            return await _reservationTypeRepository.GetByCodeAsync(code);
        }

        public async Task<Result<ReservationType>> GetReservationTypeByIdAsync(int id)
        {
            ReservationType? type = await _reservationTypeRepository.GetByIdAsync(id);

            if (type is null)
                return  Result<ReservationType>.Failure(ReservationTypeErrors.NotFound(id));

            return Result<ReservationType>.Success(type);
        }
    }
}
