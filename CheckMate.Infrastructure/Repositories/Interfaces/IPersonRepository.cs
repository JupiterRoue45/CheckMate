using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public Task<Person?> GetByIdAsync(int Id);

        public Task<IEnumerable<Person>> GetAllAsync();

        public Task CreateAsync(Person person);
    }
}
