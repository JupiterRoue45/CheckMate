using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.ReservationType;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IReservationTypeService
    {
        Task<Result<ReservationType>> CreateReservationTypeAsync(ReservationTypeCreationDto dto);

        Task<IEnumerable<ReservationType>> GetAllReservationTypesAsync();

        Task<IEnumerable<ReservationType>> GetAllReservationTypesFromCodeAsync(string code);

        Task<Result<ReservationType>> GetReservationTypeByIdAsync(int id);
    }
}
