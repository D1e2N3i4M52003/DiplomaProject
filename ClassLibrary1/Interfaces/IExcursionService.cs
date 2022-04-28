using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Business.JSONModels;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IExcursionService
    {
        Task<ExcursionModel> GetById(Guid id);
        Task<ExcursionModel> GetByAsync(Expression<Func<Excursion, bool>> filter);
        Task CreateAsync(ExcursionModel model);
        Task EditAsync(ExcursionModel model);
        Task DeleteAsync(Guid id);
        Task<List<ExcursionModel>> GetAll();
        Task<List<ExcursionModel>> GetAll(Expression<Func<Excursion, bool>> filter);

    }
}
