using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<ICollection<T>> GetAll();
        Task<ICollection<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(Guid id);
        ValueTask<ICollection<T>> GetByAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(T entity);
    }
}
