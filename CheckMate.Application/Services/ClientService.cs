using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Client;
using CheckMate.Application.Errors.Client;
using CheckMate.Application.Helpers;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Domain.Enums;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<IEnumerable<ClientDto>> GeAllClientsAsync()
        {
            IEnumerable<Client> clients = await _clientRepository.GetAllAsync();

            return clients.Select(c => c.ToDto());
        }

        public async Task<Result<ClientDto>> GetClientByIdAsync(int id)
        {
            Client? client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
            {
                return Result<ClientDto>.Failure(ClientErrors.CLientNotFound(id));
            }

            return Result<ClientDto>.Success(client.ToDto());
        }
    }
}
