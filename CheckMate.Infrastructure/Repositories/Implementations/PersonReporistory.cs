using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private readonly CheckMateDbContext _context;

        public PersonRepository(CheckMateDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.AsNoTracking().ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int Id)
        {
            return await _context.Persons.FindAsync(Id);
        }
    }
}