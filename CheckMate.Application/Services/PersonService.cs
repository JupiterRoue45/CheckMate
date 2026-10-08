using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Person;
using CheckMate.Application.Errors.Person;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Person>> CreatePersonAsync(PersonCreationDto dto)
        {
            Person person = new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsAdult = dto.IsAdult.Value,
                PhoneAreaCode = dto.PhoneAreaCode,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Gender = dto.Gender.Value
            };

            await _personRepository.CreateAsync(person);

            await _unitOfWork.SaveChangesAsync();

            return Result<Person>.Success(person);
        }

        public async Task<Result<Person>> GetPersonByIdAsync(int Id)
        {
            Person? person = await _personRepository.GetByIdAsync(Id);

            if (person == null)
            {
                return Result<Person>.Failure(PersonErrors.PersonNotFound(Id));
            }
            return Result<Person>.Success(person);
        }

        public async Task<IEnumerable<Person>> ListAllPersonsAsync()
        {
            return await _personRepository.GetAllAsync();
        }
    }
}
