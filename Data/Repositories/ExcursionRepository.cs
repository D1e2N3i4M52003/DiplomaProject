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
    public class ExcursionRepository : BaseRepository<Excursion>, IExcursionRepository
    {
        public ExcursionRepository(DBContext context) : base(context)
        {
        }

        public override async Task<ICollection<Excursion>> GetAll()
        {
            return await _context.Set<Excursion>().Include(e=> e.Destinations).ToListAsync();
        }
        public override async Task<ICollection<Excursion>> GetAll(Expression<Func<Excursion, bool>> filter)
        {
            return await _context.Set<Excursion>().Include(e => e.Destinations).Where(filter).ToListAsync();
        }
        public override async ValueTask<ICollection<Excursion>> GetByAsync(Expression<Func<Excursion, bool>> filter)
        {
            return await _context.Set<Excursion>().Include(e => e.Destinations).Where(filter).ToListAsync();
        }
        public override async Task<Excursion> GetByIdAsync(Guid id)
        {
            return await _context.Set<Excursion>().Include(e => e.Destinations).SingleAsync(e=>e.Id==id);
        }
        public override async Task UpdateAsync(Excursion entity)
        {
            Excursion dbEntity = await GetByIdAsync(entity.Id);

            if (dbEntity == null)
            {
                throw new ArgumentException();
            }
            if(dbEntity.Destinations is null)
            { 
                dbEntity.Destinations = new List<Destinations>(); }
            foreach(var destination in entity.Destinations)
            {
                dbEntity.Destinations.Add(destination);
            }
            _context.Update(dbEntity);

            await _context.SaveChangesAsync();
        }
    }
}
