using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Client;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GeAllClientsAsync();

        Task<Result<ClientDto>> GetClientByIdAsync(int id);
    }
}
