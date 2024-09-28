using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.persons.ToListAsync();
        }

        public async Task<Person> GetById(int id)
        {
            return await _context.persons.FindAsync(id);
        }

        public async Task Add(Person person)
        {
            await _context.persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Person person)
        {
            _context.persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var person = await _context.persons.FindAsync(id);
            if (person != null)
            {
                _context.persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
        public Person GetByIdentificationNumber(string identificationNumber)
        {
            return _context.persons
                .FirstOrDefault(p => p.NumberDocument == identificationNumber);
        }
    }
}
