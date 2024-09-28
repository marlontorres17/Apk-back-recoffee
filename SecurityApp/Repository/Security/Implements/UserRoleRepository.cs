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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            return await _context.userRoles.ToListAsync();
        }

        public async Task<UserRole> GetById(int id)
        {
            return await _context.userRoles.FindAsync(id);
        }

        public async Task Add(UserRole userRole)
        {
            await _context.userRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserRole userRole)
        {
            _context.userRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userRole = await _context.userRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.userRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}