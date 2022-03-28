using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DBContext _context;

        public BaseRepository(DBContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter);
        }
        public virtual ValueTask<T?> GetByAsync(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FindAsync(filter);
        }
        public virtual async ValueTask<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var dbEntity = await GetByIdAsync(entity.Id);

            if (dbEntity == null)
            {
                throw new ArgumentException();
            }

            _context.Entry(dbEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentException();
            }

            _context.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
