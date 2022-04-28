using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using DataLayer.Interfaces;
using System.Linq.Expressions;

namespace DataLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DBContext context) : base(context)
        {
        }
        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Set<User>().Include(e => e.Excursions).SingleAsync(e => e.Id == id);
        }
        public override async Task UpdateAsync(User entity)
        {
            User dbEntity = await GetByIdAsync(entity.Id);

            if (dbEntity == null)
            {
                throw new ArgumentException();
            }
            if (dbEntity.Excursions is null)
            {
                dbEntity.Excursions = new List<Excursion>();
            }
            foreach (var excursion in entity.Excursions)
            {
                dbEntity.Excursions.Add(excursion);
            }
            _context.Update(dbEntity);
            
            await _context.SaveChangesAsync();
        }
    }
}
