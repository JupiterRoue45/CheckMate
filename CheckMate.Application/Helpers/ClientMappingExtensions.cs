using CheckMate.Application.DTOs.Client;
using CheckMate.Domain.Entities;
using CheckMate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Helpers
{
    public static class ClientMappingExtensions
    {
        public static ClientDto ToDto(this Client client)
        {
            return new ClientDto
            {
                Id = client.ClientId,
                Name = client is Person person ? $"{person.FirstName} {person.LastName}" : ((Company)client).CompanyName,
                Type = client is Person ? ClientTypeEnum.Person : ClientTypeEnum.Company,
                Email = client.Email,
                PhoneAreaCode = client.PhoneAreaCode,
                PhoneNumber = client.PhoneNumber
            };
        }
    }
}
