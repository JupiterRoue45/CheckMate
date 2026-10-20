using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IReservationTypeRepository
    {
        Task<ReservationType?> GetByIdAsync(int id);

        Task<IEnumerable<ReservationType>> GetByCodeAsync(string code);

        Task<IEnumerable<ReservationType>> GetAllAsync();

        Task CreateAsync(ReservationType type);

        Task<bool> VerifyCodeExistance(string code);
    }
}
