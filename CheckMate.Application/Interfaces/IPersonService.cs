using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Client;
using CheckMate.Application.DTOs.Person;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IPersonService
    {
        Task<Result<ClientDto>> GetPersonByIdAsync(int Id);

        Task<IEnumerable<ClientDto>> ListAllPersonsAsync();

        Task<Result<Person>> CreatePersonAsync(PersonCreationDto dto);
    }
}
