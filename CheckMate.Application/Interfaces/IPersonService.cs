using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Person;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IPersonService
    {
        Task<Result<Person>> GetPersonByIdAsync(int Id);

        Task<IEnumerable<Person>> ListAllPersonsAsync();

        Task<Result<Person>> CreatePersonAsync(PersonCreationDto dto);
    }
}
