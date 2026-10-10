using CheckMate.Application.Common.Results;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IComplementaryServiceDeliveryService
    {
        Task<Result<ComplementaryService>> CreateComplementaryServiceAsync();

        Task<IEnumerable<ComplementaryService>> GetComplementaryServicesForReservationRoomAsync();

        Task<IEnumerable<ComplementaryService>> GetComplementaryServicesAsync();
    }
}
