using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IComplementaryServiceRepository
    {
        Task CreateAsync(ComplementaryService complementaryService);

        Task<IEnumerable<ComplementaryService>> GetAllComplementaryServicesOfAReservationRoom(int reservationRoomId);

        Task<IEnumerable<ComplementaryService>> GetAllAsync();

    }
}
